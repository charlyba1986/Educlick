using CapaDatos;
using CapaEntidad;
using CapaNegocio;
using CapaPresentacion.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class AlumnoController : Controller
    {
        // GET: Alumno
        //public ActionResult Index()
        //{
        //    return View();
        //}
        public ActionResult Index()
        {
            int idUsuario = Convert.ToInt32(Session["UsuarioId"]);

            AlumnoData alumnoData = new AlumnoData();
            List<CapaEntidad.Curso> cursosInscriptos = alumnoData.VerificarCursosInscriptos(idUsuario);

            return View(cursosInscriptos);
        }

        public ActionResult MiProgreso()
        {
            int idUsuario = Convert.ToInt32(Session["usuarioID"]);
            var negocio = new AlumnoNegocio();
            DashboardAlumnoDto dto = negocio.ObtenerDashboardAlumno(idUsuario);

            var vm = new DashboardAlumnoVM
            {
                HorasTotales = dto.HorasTotales,
                HorasCompletadas = dto.HorasCompletadas,
                PlanesActivos = dto.PlanesActivos,
                Rutas = dto.Rutas.Select(r => new RutaProgresoVM
                {
                    IdRuta = r.IdRuta,
                    NombreRuta = r.NombreRuta,
                    HorasTotales = r.HorasTotales,
                    HorasCompletadas = r.HorasCompletadas
                }).ToList()
            };

            return View(vm);
            //return View();
        }

        public ActionResult MisRutas()
        {
            // 1) Obtener el alumno logueado (si no hay sesión, devolvé lista vacía)
            int idAlumno = 0;
            if (Session["usuarioID"] != null)
            {
                int.TryParse(Session["usuarioID"].ToString(), out idAlumno);
            }

            var negocio = new RutaNegocio();
            List<Ruta> rutas = new List<Ruta>();

            if (idAlumno > 0)
            {
                rutas = negocio.ListarRutasParaAlumno(idAlumno) ?? new List<Ruta>();
            }

            // 2) DEVOLVÉ SIEMPRE una lista (no null)
            return View(rutas);
            
        }
        public ActionResult Certificados()
        {
            return View();
        }

        public ActionResult ChatEduIa()
        {
            return View();
        }

        public ActionResult VerPerfil()
        {
            return View();
        }

        public ActionResult NuevoCurso()
        {
            try
            {
                CursoData cursoData = new CursoData();
                var cursos = cursoData.ObtenerCursos();
                return View(cursos);
            }
            catch (Exception ex)
            {
                TempData["Mensaje"] = "Error al cargar los cursos: " + ex.Message;
                return View(new List<Curso>());
            }
           
        }

       
        public ActionResult InscribirseEnCurso(int idCurso)
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["usuarioID"]);
                AlumnoData alumno = new AlumnoData();
                bool exito = alumno.InscribirseEnCurso(idUsuario, idCurso);

                if(exito)
                {
                    TempData["Mensaje"] = "Te inscribiste correctamente en el curso";
                }
                else
                {
                    TempData["Mensaje"] = "No se pudo complpetar la inscripcion";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Mensaje"]= "Error al inscribirse"+ex.Message;
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult IniciarRuta(int idRuta)
        {
            try
            {
                int idUsuario = Convert.ToInt32(Session["usuarioID"]);
                var negocio = new CapaNegocio.RutaNegocio();
                bool exito = negocio.IniciarRuta(idUsuario, idRuta);

                if (exito)
                {
                    TempData["MensajeRuta"] = "ok";
                }
                else
                {
                    TempData["MensajeRuta"] = "error";
                }

                return RedirectToAction("MisRutas","Alumno"); // vuelve a la vista con las rutas
            }
            catch (Exception ex)
            {
                TempData["MensajeRuta"] = "error";
                Console.WriteLine(ex.Message);
                return RedirectToAction("Rutas");
            }
        }



        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesion", "Home");
        }


     

























    }
}