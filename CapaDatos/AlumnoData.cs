using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class AlumnoData
    {
        private readonly string connectionString = @"Data Source=JAIRQUIROGAASSA\SQLEXPRESS;Initial Catalog=Educlick;Integrated Security=True;TrustServerCertificate=True";
        public List<CapaEntidad.Curso> VerificarCursosInscriptos(int idUsuario)
        {
            List<CapaEntidad.Curso> cursos = new List<CapaEntidad.Curso>();
         

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            SELECT c.IdCurso, c.Nombre, c.Descripcion, c.DuracionHoras
            FROM Curso c
            INNER JOIN UsuarioCurso uc ON c.IdCurso = uc.IdCurso
            WHERE uc.IdUsuario = @idUsuario";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);

                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    cursos.Add(new CapaEntidad.Curso
                    {
                        IdCurso = Convert.ToInt32(reader["IdCurso"]),
                        Nombre = reader["Nombre"].ToString(),
                        Descripcion = reader["Descripcion"].ToString(),
                        DuracionHoras = Convert.ToInt32(reader["DuracionHoras"])
                    });
                }
            }

            return cursos;
        }
        public bool InscribirseEnCurso(int idUsuario, int idCurso)
        {
            bool exito = false;            

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = @"
            INSERT INTO UsuarioCurso (IdUsuario, IdCurso, FechaInscripcion, Estado)
            VALUES (@idUsuario, @idCurso, GETDATE(), 'Activo')";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@idUsuario", idUsuario);
                command.Parameters.AddWithValue("@idCurso", idCurso);

                connection.Open();
                int filas = command.ExecuteNonQuery();

                exito = filas > 0;
            }

            return exito;
        }


       public DashboardAlumnoDto ObtenerDashboardAlumno(int idUsuario)
        {
            var dto= new DashboardAlumnoDto();
            var rutas= new List<RutaProgresoDto>();

            using(var con =new SqlConnection(connectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(@"
                SELECT r.IdRuta, r.Nombre, ISNULL(SUM(c.Horas),0) AS HorasTotales
                FROM UsuarioRuta ur
                JOIN Ruta r ON r.IdRuta = ur.IdRuta AND r.Activo = 1
                LEFT JOIN CursoRuta cr ON cr.IdRuta = r.IdRuta
                LEFT JOIN Curso c ON c.IdCurso = cr.IdCurso
                WHERE ur.IdUsuario = @u
                GROUP BY r.IdRuta, r.Nombre
                ORDER BY r.Nombre;", con))
                {
                    cmd.Parameters.AddWithValue("@u", idUsuario);
                    using (SqlDataReader rd = cmd.ExecuteReader())
                        
                    {
                        while (rd.Read())
                        {     
                            rutas.Add(new RutaProgresoDto
                        {
                            IdRuta = rd.GetInt32(0),
                            NombreRuta = rd.GetString(1),
                            HorasTotales = rd.GetInt32(2)
                        });
                        }
                    }
                }
                foreach (RutaProgresoDto r in rutas)
                {
                    using (SqlCommand cmd2 = new SqlCommand(@"
                        SELECT ISNULL(SUM(c.Horas * ISNULL(ucp.Porcentaje,0)/100.0),0)
                        FROM CursoRuta cr
                        JOIN Curso c ON c.IdCurso = cr.IdCurso
                        LEFT JOIN UsuarioCursoProgreso ucp
                               ON ucp.IdUsuario = @u AND ucp.IdCurso = cr.IdCurso
                        WHERE cr.IdRuta = @r;", con))
                    {
                        cmd2.Parameters.AddWithValue("@u", idUsuario);
                        cmd2.Parameters.AddWithValue("@r", r.IdRuta);

                        object result = cmd2.ExecuteScalar();
                        double horas = (result == null || result == DBNull.Value) ? 0.0 : Convert.ToDouble(result);
                        r.HorasCompletadas = (int)Math.Round(horas);
                    }
                }
            }
            dto.Rutas = rutas;
            dto.PlanesActivos = rutas.Count;
            dto.HorasTotales = rutas.Sum(x => x.HorasTotales);
            dto.HorasCompletadas = rutas.Sum(x => x.HorasCompletadas);
            return dto;
        }
    }
}
