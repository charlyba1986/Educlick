using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class Curso
    {
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
    }
}
