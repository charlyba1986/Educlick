using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class Ruta
    {
        public int IdRuta { get; set; }
        public string Codigo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }
        public bool IniciadaPorAlumno { get; set; }

        public int CantidadCursos { get; set; }
        public int ProgresoPorcentaje { get; set; }

    }
}
