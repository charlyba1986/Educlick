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
        public ActionResult Index()
        {
            return View();
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
    }
}