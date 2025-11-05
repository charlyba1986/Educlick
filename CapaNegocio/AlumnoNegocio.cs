using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaDatos;
using CapaEntidad;

namespace CapaNegocio
{
    public class AlumnoNegocio
    {
        private readonly AlumnoData _datos= new AlumnoData();

        public DashboardAlumnoDto ObtenerDashboardAlumno (int idUsuario)
        {
            return _datos.ObtenerDashboardAlumno(idUsuario);
        }
    }
}
