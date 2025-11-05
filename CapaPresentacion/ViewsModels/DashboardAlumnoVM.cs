using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaPresentacion.ViewsModels
{
    public class DashboardAlumnoVM
    {
        public int HorasTotales { get; set; }
        public int HorasCompletadas { get; set; }
        public int PlanesActivos { get; set; }         // rutas activas del alumno
        public int ProgresoGeneral => HorasTotales == 0 ? 0 : (int)Math.Round((double)HorasCompletadas * 100 / HorasTotales);
        public List<RutaProgresoVM> Rutas { get; set; } = new List<RutaProgresoVM>();
    }
}