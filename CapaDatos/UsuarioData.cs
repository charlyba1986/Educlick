using CapaEntidad;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class UsuarioData
    {

        private readonly string connectionString = @"Data Source=JAIRQUIROGAASSA\SQLEXPRESS;Initial Catalog=Educlick;Integrated Security=True;TrustServerCertificate=True";
        SqlDataAdapter adapter = new SqlDataAdapter();
        public bool AltaUsuario(Usuario usuario)
        {
            // Validar datos básicos
            if (usuario == null || string.IsNullOrWhiteSpace(usuario.Email) || string.IsNullOrWhiteSpace(usuario.Password))
                return false;

            // Simulación de inserción en base de datos usando ADO.NET
            try
            {
                using (var connection = new System.Data.SqlClient.SqlConnection(connectionString))
                {
                    connection.Open();
                    using (var command = new System.Data.SqlClient.SqlCommand(
                        "INSERT INTO Usuarios (Email, Password, Rol, UsuarioSistema,Nombre, Apellido, Telefono) VALUES (@Email, @Password, @Rol,@UsuarioSistema, @Nombre,@Apellido, @Telefono)", connection))
                    {
                        command.Parameters.AddWithValue("@Email", usuario.Email);
                        command.Parameters.AddWithValue("@Password", usuario.Password);
                        command.Parameters.AddWithValue("@Rol", usuario.Rol);
                        command.Parameters.AddWithValue("@UsuarioSistema", usuario.UsuarioSistema);
                        command.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                        command.Parameters.AddWithValue("@Apellido", usuario.Apellido);
                        command.Parameters.AddWithValue("@Telefono", usuario.Telefono);

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

        public Usuario ConsultarUsuario(Usuario usuario)
        {
            Usuario resultado = null;
            string conexionString = connectionString;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "";
                SqlCommand command = new SqlCommand();
                command.Connection = connection;

                if (usuario.Id == 0)
                {
                    query = "SELECT u.Id, Email, Password, IdRol,r.Rol, UsuarioSistema, Nombre, Apellido, Telefono FROM Usuario AS u LEFT JOIN Rol AS r ON u.IdRol = r.Id WHERE UsuarioSistema = @usuario";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@usuario", usuario.UsuarioSistema);
                }
                else
                {
                    query = "SELECT Id, Email, Password, Rol, UsuarioSistema, Nombre, Apellido, Telefono FROM Usuario WHERE Id = @id";
                    command.CommandText = query;
                    command.Parameters.AddWithValue("@id", usuario.Id);
                }

                SqlDataAdapter adapter = new SqlDataAdapter(command);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    resultado = new Usuario
                    {
                        Id = Convert.ToInt32(row["Id"]),
                        Email = row["Email"].ToString(),
                        Password = row["Password"].ToString(),
                        Rol = row["Rol"].ToString(),
                        UsuarioSistema = row["UsuarioSistema"].ToString(),
                        Nombre = row["Nombre"].ToString(),
                        Apellido = row["Apellido"].ToString(),
                        Telefono = row["Telefono"] == DBNull.Value ? 0 : Convert.ToInt32(row["Telefono"])
                    };
                }
            }

            return resultado;
        }
    }
}
