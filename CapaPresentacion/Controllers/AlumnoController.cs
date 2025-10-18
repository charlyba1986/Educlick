using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaDatos;

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
            return View();
        }

        public ActionResult MisRutas()
        {
            return View();
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
            return View();
        }

        [HttpPost]
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
    }
}