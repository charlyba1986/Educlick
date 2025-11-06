using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class ProfesorNegocio
    {
        private readonly ProfesorData _datos = new ProfesorData();
        public ProfesorSeguimientoDto ObtenerSeguimientoPlanes(int? idRuta = null)
        {
            return _datos.ObtenerSeguimientoPlanes(idRuta);
        }
    }
}
    