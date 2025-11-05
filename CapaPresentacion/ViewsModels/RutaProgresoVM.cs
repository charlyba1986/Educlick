using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapaPresentacion.ViewsModels
{
    public class RutaProgresoVM
    {
    public int IdRuta { get; set; }
        public string NombreRuta { get; set; }
        public int HorasTotales { get; set; }          // suma horas de cursos de la ruta
        public int HorasCompletadas { get; set; }      // horasTotales * progreso (si hay tracking)
        public int Porcentaje => HorasTotales == 0 ? 0 : (int)Math.Round((double)HorasCompletadas * 100 / HorasTotales);
    }
}
