using System;
using System.Collections.Generic;
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
    }
}
