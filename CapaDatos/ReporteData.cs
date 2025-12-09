using CapaEntidad;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class ReporteData
    {
        private readonly string _connectionString = @"Data Source=JAIRQUIROGAASSA\SQLEXPRESS;Initial Catalog=Educlick;Integrated Security=True;TrustServerCertificate=True";

        /// <summary>
        /// Devuelve el último progreso por alumno-curso (una fila por par).
        /// Filtros opcionales: idProfesor, idCurso, idAlumno, rango fechas.
        /// </summary>
        public List<ProgresoAlumnoDTO> ListarProgreso(
            int? idProfesor = null,
            int? idCurso = null,
            int? idAlumno = null,
            DateTime? desde = null,
            DateTime? hasta = null)
        {
            var lista = new List<ProgresoAlumnoDTO>();

            using (var cn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(@"
                                ;WITH Ult AS (
                                    SELECT
                                        ucp.IdUsuario,
                                        ucp.IdCurso,
                                        ucp.Porcentaje,
                                        ucp.UltimaActividad,
                                        ROW_NUMBER() OVER (PARTITION BY ucp.IdUsuario, ucp.IdCurso
                                                           ORDER BY ucp.UltimaActividad DESC, ucp.IdProg DESC) AS rn
                                    FROM dbo.UsuarioCursoProgreso ucp
                                    WHERE (@idCurso   IS NULL OR ucp.IdCurso   = @idCurso)
                                      AND (@idAlumno  IS NULL OR ucp.IdUsuario = @idAlumno)
                                      AND (@desde     IS NULL OR ucp.UltimaActividad >= @desde)
                                      AND (@hasta     IS NULL OR ucp.UltimaActividad <  @hasta)
                                )
                                SELECT
                                    u.IdUsuario,
                                    (u.Apellido + ', ' + u.Nombre) AS Alumno,
                                    c.IdCurso,
                                    c.Nombre AS Curso,
                                    ISNULL(u2.Porcentaje,0) AS Porcentaje,
                                    ISNULL(u2.UltimaActividad, '19000101') AS UltimaActividad
                                FROM dbo.Curso c
                                INNER JOIN dbo.UsuarioCurso uc ON uc.IdCurso = c.IdCurso           -- si manejás inscripciones
                                INNER JOIN dbo.Usuario u ON u.IdUsuario = uc.IdUsuario
                                LEFT JOIN Ult u2 ON u2.IdUsuario = uc.IdUsuario AND u2.IdCurso = uc.IdCurso AND u2.rn = 1
                                WHERE (@idProfesor IS NULL OR c.IdProfesor = @idProfesor)
                                  AND (@idCurso    IS NULL OR c.IdCurso    = @idCurso)
                                  AND (@idAlumno   IS NULL OR u.IdUsuario  = @idAlumno)
                                ORDER BY c.Nombre, Alumno;", cn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add("@idProfesor", SqlDbType.Int).Value = (object)idProfesor ?? DBNull.Value;
                cmd.Parameters.Add("@idCurso", SqlDbType.Int).Value = (object)idCurso ?? DBNull.Value;
                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = (object)idAlumno ?? DBNull.Value;
                cmd.Parameters.Add("@desde", SqlDbType.DateTime).Value = (object)desde ?? DBNull.Value;
                cmd.Parameters.Add("@hasta", SqlDbType.DateTime).Value = (object)hasta ?? DBNull.Value;

                cn.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ProgresoAlumnoDTO
                        {
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"]),
                            Alumno = dr["Alumno"].ToString(),
                            IdCurso = Convert.ToInt32(dr["IdCurso"]),
                            Curso = dr["Curso"].ToString(),
                            Porcentaje = Convert.ToInt32(dr["Porcentaje"]),
                            UltimaActividad = Convert.ToDateTime(dr["UltimaActividad"])
                        });
                    }
                }
            }

            return lista;
        }
    }
}

