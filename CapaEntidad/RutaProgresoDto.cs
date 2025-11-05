using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class RutaProgresoDto
    {
        public int IdRuta { get; set; }
        public string NombreRuta { get; set; }
        public int HorasTotales { get; set; }
        public int HorasCompletadas { get; set; }
        public int Porcentaje =>
            HorasTotales == 0 ? 0 :
            (int)Math.Round((double)HorasCompletadas * 100 / HorasTotales);
    }
}
