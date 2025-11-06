using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class AlumnoPlanSeguimientoDto
    {
        public int IdUsuario { get; set; }
        public string Alumno { get; set; }
        public int IdRuta { get; set; }
        public string NombreRuta { get; set; }
        public int PorcentajeRuta { get; set; }
        public bool AbandonoFlag { get; set; }
    }
}
