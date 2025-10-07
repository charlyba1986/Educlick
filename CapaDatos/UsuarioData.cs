using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
    public class UsuarioData
    {
        public bool AltaUsuario(Usuario usuario)
        {
            // Validar datos básicos
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Password))
                return false;

            // Simulación de inserción en base de datos usando ADO.NET
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection("tu_cadena_conexion"))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO Usuarios (Email, Password, Rol) VALUES (@Email, @Password, @Rol)", connection))
                    {
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@Password", usuario.Password);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol);

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


        public bool BajaUsuario(int id)
        {
            if (id <= 0)
                return false;
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection("tu_cadena_conexion"))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "DELETE FROM Usuarios WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", id);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ModificarUsuario(Usuario usuario)
        {
            if (usuario == null || usuario.Id <= 0 || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Password))
                return false;
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection("tu_cadena_conexion"))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "UPDATE Usuarios SET Email = @Email, Password = @Password, Rol = @Rol WHERE Id = @Id", connection))
                    {
                        command.Parameters.AddWithValue("@Id", usuario.Id);
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@Password", usuario.Password);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol);
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
