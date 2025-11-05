using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;

namespace CapaNegocio
{
    public class RutaNegocio
    {
        private RutaDatos datos = new RutaDatos();

        public bool IniciarRuta(int idUsuario, int idRuta)
        {
            return datos.RegistrarInicioRuta(idUsuario, idRuta);
        }

        public List<CapaEntidad.Ruta> ListarRutasParaAlumno(int idAlumno)
        {
            return datos.ListarRutasParaAlumno(idAlumno);
        }
    }
}
