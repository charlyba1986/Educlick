using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Rol { get; set; }

        public string UsuarioSistema { get; set; }
        public string  Nombre { get; set; }
        public string Apellido { get; set; }
        public  int Telefono { get; set; }

        public  List<UsuarioCurso> UsuarioCursos { get; set; }
    }
}
