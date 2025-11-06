using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ProfesorSeguimientoDto
    {
        public int PromedioAvance { get; set; }
        public int Participacion { get; set; }
        public int Abandono { get; set; }
        public int Activos { get; set; }
        public int Finalizados { get; set; }
        public int SinAvances { get; set; }
        public int EnRevision { get; set; }

        public List<AlumnoPlanSeguimientoDto> AlumnoDetalle { get; set; } = new List<AlumnoPlanSeguimientoDto>();
    }
}
