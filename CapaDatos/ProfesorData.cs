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

        public bool GuardarCurso (CapaEntidad.Curso curso, int idProfesor)
        {
            bool exito = false;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = @"INSERT INTO Curso (Nombre, Descripcion, DuracionHoras, IdProfesor) VALUES (@nombre, @descripcion, @duracionHoras, @idProfesor)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", curso.Nombre);
                command.Parameters.AddWithValue("@descripcion", curso.Descripcion);
                command.Parameters.AddWithValue("@DuracionHoras",curso.DuracionHoras);
                command.Parameters.AddWithValue("@IdProfesor", idProfesor);

                int filas =command.ExecuteNonQuery();

                exito = filas > 0;
            }
            return exito;
        }
    }
}
