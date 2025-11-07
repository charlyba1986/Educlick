using CapaDatos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaNegocio;
using CapaEntidad;

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

        public ActionResult SeguimientoAlumnos (int ? idRuta)
        {
            var negocio = new ProfesorNegocio();
            ProfesorSeguimientoDto dto = negocio.ObtenerSeguimientoPlanes(idRuta);
            return View(dto);
        }

        public ActionResult Alertas()
        {
            return View();
        }

        public ActionResult GuardarCurso(CapaEntidad.Curso curso)
        {
            try
            {
                int idProfesor = Convert.ToInt32(Session["UsuarioID"]);
                ProfesorData profesor = new ProfesorData();
                bool exito = profesor.GuardarCurso(curso, idProfesor);

                if (exito)
                {
                    TempData["Mensaje"] = "El curso fue creado correctamente";
                }
                else
                {
                    TempData["Mensaje"] = "No se puede guardar el curso";
                }
                return RedirectToAction("NuevoCurso");
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult NuevoCurso()
        {
            return View();
        }

        public ActionResult CerrarSesion()
        {
            Session.Clear();
            return RedirectToAction("IniciarSesion", "Home");
        }



    }
}
