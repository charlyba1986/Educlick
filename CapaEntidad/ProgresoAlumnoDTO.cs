using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidad
{
    public class ProgresoAlumnoDTO
    {

        public int IdUsuario { get; set; }
        public string Alumno { get; set; }

        public int IdCurso { get; set; }
        public string Curso { get; set; }

        // Último registro de avance para ese alumno-curso
        public int Porcentaje { get; set; }          // 0..100
        public System.DateTime UltimaActividad { get; set; }

        public class KPIsProgresoDTO
        {
            public int CantAlumnos { get; set; }
            public int CantCursos { get; set; }
            public double PromedioAvance { get; set; }   // 0..100
            public double Participacion { get; set; }    // % alumnos con Porcentaje>0
            public int CapacidadOcupada { get; set; }    // si querés un gauge opcional
        }
    }
}
