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
        public List<CapaEntidad.Curso> VerificarCursosInscriptos(int idUsuario)
        {
            List<CapaEntidad.Curso> cursos = new List<CapaEntidad.Curso>();
            string connectionString = "TU_CONNECTION_STRING";

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

    }
}
