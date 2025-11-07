using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class ProfesorData
    {
        private readonly string connectionString = @"Data Source=JAIRQUIROGAASSA\SQLEXPRESS;Initial Catalog=Educlick;Integrated Security=True;TrustServerCertificate=True";

        public bool GuardarCurso(CapaEntidad.Curso curso, int idProfesor)
        {
            bool exito = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Curso (Nombre, Descripcion, DuracionHoras, IdProfesor) VALUES (@nombre, @descripcion, @duracionHoras, @idProfesor)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", curso.Nombre);
                command.Parameters.AddWithValue("@descripcion", curso.Descripcion);
                command.Parameters.AddWithValue("@DuracionHoras", curso.DuracionHoras);
                command.Parameters.AddWithValue("@IdProfesor", idProfesor);

                int filas = command.ExecuteNonQuery();

                exito = filas > 0;
            }
            return exito;
        }


        /// <summary>
        /// Trae seguimiento de planes (rutas) por alumno.
        /// Si idRuta es null, trae todas las rutas.
        /// </summary>
        /// 
        public ProfesorSeguimientoDto ObtenerSeguimientoPlanes(int? idRuta = null)
        {
            var dto = new ProfesorSeguimientoDto();
            var detalle = new List<AlumnoPlanSeguimientoDto>();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                string query = @"SELECT
                                u.Id ,
                                (u.Nombre + ' ' + u.Apellido) AS Alumno,
                                 r.IdRuta,
                                 r.Nombre AS NombreRuta,
                                  CAST(
                                  CASE WHEN SUM(ISNULL(c.DuracionHoras,0)) = 0 THEN 0
                                  ELSE (SUM(ISNULL(c.DuracionHoras,0) * ISNULL(ucp.Porcentaje,0)) * 1.0) / SUM(ISNULL(c.DuracionHoras,0))
                                  END
                                  AS DECIMAL(5,2)
                                  ) AS PorcentajeRuta,
                                    CASE WHEN MAX(CASE WHEN ISNULL(ucp.Estado,0)=2 THEN 1 ELSE 0 END) = 1 THEN 1 ELSE 0 END AS AbandonoFlag
                                FROM UsuarioRuta ur
                                JOIN Usuario u      ON u.Id = ur.IdUsuario
                                JOIN Ruta r         ON r.IdRuta    = ur.IdRuta
                                LEFT JOIN CursoRuta cr ON cr.IdRuta = r.IdRuta
                                LEFT JOIN Curso c      ON c.IdCurso = cr.IdCurso
                                LEFT JOIN UsuarioCursoProgreso ucp
                                    ON ucp.IdUsuario = u.Id AND ucp.IdCurso = cr.IdCurso
                                WHERE (@IdRuta IS NULL OR r.IdRuta = @IdRuta)
                                GROUP BY u.Id, u.Nombre, u.Apellido, r.IdRuta, r.Nombre
                                ORDER BY r.Nombre, Alumno;";

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@IdRuta", (object)idRuta ?? DBNull.Value);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    detalle.Add(new AlumnoPlanSeguimientoDto
                    {
                        IdUsuario = Convert.ToInt32(dr["Id"]),
                        Alumno = dr["Alumno"].ToString(),
                        IdRuta = Convert.ToInt32(dr["IdRuta"]),
                        NombreRuta = dr["NombreRuta"].ToString(),
                        PorcentajeRuta = Convert.ToInt32(dr["PorcentajeRuta"]),
                        AbandonoFlag = Convert.ToBoolean(dr["AbandonoFlag"])
                    });
                }
            }

            int total = detalle.Count;
            int activos = 0, finalizados = 0, sinAvances = 0, enRevision = 0;
            

            foreach (var d in detalle)
            {
                if (d.AbandonoFlag)
                    enRevision++;
                if (d.PorcentajeRuta == 0)
                    sinAvances++;
                else if (d.PorcentajeRuta >= 100)
                    finalizados++;
                else
                    activos++; 
            }
            if(total > 0)
            {
                dto.PromedioAvance = detalle.Sum(x => x.PorcentajeRuta) / total;
                dto.Participacion = ((total - sinAvances) * 100) / total;
            }
            else
            {
                dto.PromedioAvance = 0;
                dto.Participacion = 0;
            }

            dto.Activos = activos;
            dto.Finalizados = finalizados;
            dto.SinAvances = sinAvances;
            dto.Abandono = enRevision;
            dto.AlumnoDetalle = detalle;

            return dto;
        }
    }
}
