using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class DashboardAlumnoDto
    {
        public int HorasTotales { get; set; }
        public int HorasCompletadas { get; set; }
        public int PlanesActivos { get; set; }
        public List<RutaProgresoDto> Rutas { get; set; } = new List<RutaProgresoDto>();
        public int ProgresoGeneral =>
            HorasTotales == 0 ? 0 :
            (int)Math.Round((double)HorasCompletadas * 100 / HorasTotales);
    }
}
