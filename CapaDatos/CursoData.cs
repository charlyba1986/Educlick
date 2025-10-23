using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CursoData
    {

        private readonly string connectionString = @"Data Source=JAIRQUIROGAASSA\SQLEXPRESS;Initial Catalog=Educlick;Integrated Security=True;TrustServerCertificate=True";
        public bool CrearCurso(string nombre, string descripcion, int duracionHoras)
        {
            // Validar datos básicos
            if (string.IsNullOrWhiteSpace(nombre) || duracionHoras <= 0)
                return false;
            // Simulación de inserción en base de datos usando ADO.NET
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection("tu_cadena_conexion"))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO Cursos (Nombre, Descripcion, DuracionHoras) VALUES (@Nombre, @Descripcion, @DuracionHoras)", connection))
                    {
                        command.Parameters.AddWithValue("@Nombre", nombre);
                        command.Parameters.AddWithValue("@Descripcion", descripcion);
                        command.Parameters.AddWithValue("@DuracionHoras", duracionHoras);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                // Manejo de errores (puedes loguear la excepción)
                return false;
            }
        }


        public List<CapaEntidad.Curso> ObtenerCursos()
        {
            List<CapaEntidad.Curso> cursos = new List<CapaEntidad.Curso>();
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand("SELECT IdCurso, Nombre, Descripcion, DuracionHoras FROM Curso", connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                cursos.Add(new CapaEntidad.Curso
                                {
                                    IdCurso = Convert.ToInt32(reader["IdCurso"]),
                                    Nombre = reader["Nombre"].ToString(),
                                    Descripcion = reader["Descripcion"].ToString(),
                                    DuracionHoras = Convert.ToInt32(reader["DuracionHoras"]),
                                    //IdProfesor = Convert.ToInt32(reader["IdProfesor"])
                                });
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch
            {
                // Manejo de errores (puedes loguear la excepción)
            }
            return cursos;
        }
    }
}
