using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidad;

namespace CapaDatos
{
     public class RutaDatos
     {
    private readonly string connectionString = @"Data Source=JAIRQUIROGAASSA\SQLEXPRESS;Initial Catalog=Educlick;Integrated Security=True;TrustServerCertificate=True";
    //      public bool RegistrarInicioRuta (int idUsuario, int idRuta)
    //      {
    //	try
    //	{
    //		using (SqlConnection con = new SqlConnection (connectionString))
    //		{

    //			SqlCommand cmd = new SqlCommand("INSERT INTO UsuarioRuta (IdUsuario, IdRuta, FechaInicio) VALUES (@alumno, @ruta, GETDATE())", con);
    //			cmd.Parameters.AddWithValue("@alumno", idUsuario);
    //			cmd.Parameters.AddWithValue("@ruta", idRuta);
    //                  con.Open();
    //			cmd.ExecuteNonQuery();
    //			con.Close();
    //			return true;
    //              }

    //	}
    //	catch (Exception ex )
    //	{

    //		throw ex;
    //	}
    //      }
    public bool RegistrarInicioRuta(int idUsuario, int idRuta)
    {
        using (var con = new SqlConnection(connectionString))
        using (var cmd = new SqlCommand(@"
        INSERT INTO UsuarioRuta (IdUsuario, IdRuta, FechaInicio)
        SELECT @alumno, @ruta, GETDATE()
        WHERE NOT EXISTS (
            SELECT 1 
            FROM UsuarioRuta 
            WHERE IdUsuario = @alumno AND IdRuta = @ruta
        );", con))
        {
            cmd.Parameters.Add("@alumno", SqlDbType.Int).Value = idUsuario;
            cmd.Parameters.Add("@ruta", SqlDbType.Int).Value = idRuta;

            con.Open();
            int rows = cmd.ExecuteNonQuery();   // 1 si insertó, 0 si ya estaba inscripto
            return rows > 0;
        }
    }




    public List<Ruta> ListarRutasParaAlumno(int idAlumno)
        {
            var lista = new List<Ruta>();

            using (var con = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(@"
        SELECT 
            r.IdRuta,
            r.Codigo,
            r.Nombre,
            r.Descripcion,
            r.Activo,
            r.FechaCreacion,
            CASE WHEN ur.IdUsuario IS NULL THEN 0 ELSE 1 END AS IniciadaPorAlumno
        FROM dbo.Ruta r
        LEFT JOIN dbo.UsuarioRuta ur
            ON ur.IdRuta = r.IdRuta AND ur.IdUsuario = @idAlumno
        WHERE r.Activo = 1
        ORDER BY r.Nombre;", con))
            {
                cmd.CommandType = CommandType.Text;

                // 🔧 ¡El nombre del parámetro debe coincidir con el del SQL!
                cmd.Parameters.Add("@idAlumno", SqlDbType.Int).Value = idAlumno;

                con.Open();
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Ruta
                        {
                            IdRuta = Convert.ToInt32(dr["IdRuta"]),
                            Codigo = dr["Codigo"].ToString(),
                            Nombre = dr["Nombre"].ToString(),
                            Descripcion = dr["Descripcion"] == DBNull.Value ? null : dr["Descripcion"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"]),
                            FechaCreacion = Convert.ToDateTime(dr["FechaCreacion"]),
                            IniciadaPorAlumno = Convert.ToInt32(dr["IniciadaPorAlumno"]) == 1
                        });
                    }
                }
            }

            return lista;
        }
    }
    
}
