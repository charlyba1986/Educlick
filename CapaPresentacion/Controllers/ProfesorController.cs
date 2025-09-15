using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class ProfesorController : Controller
    {
        // GET: Profesor
        public ActionResult Index()
        {
            return View();
        }

        // GET: Profesor/Details/5
        public ActionResult Details()
        {
            return View();
        }

        // GET: Profesor/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Profesor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Profesor/Edit/5
        public ActionResult EditarPerfil()
        {
            return View();
        }

        // Get: Profesor/Edit/5
    
        public ActionResult VerReportes()
        {
            {
                return View();
            }
        }

        public ActionResult SeguimientoAlumnos ()
        {
            return View();
        }

        public ActionResult Alertas()
        {
            return View();
        }

       
    
    }
}
