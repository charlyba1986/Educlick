using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CapaPresentacion.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult IniciarSesion()
        {
            
            return View();
        }

        [HttpPost]
        public ActionResult VerificarUsuario(string Usuario, string Contra)
        {
            // Aquí iría la lógica para verificar el usuario y la contraseña
            // Por simplicidad, asumimos que el usuario es "admin" y la contraseña es "password"
            if (Usuario == "admin@gmail.com" && Contra == "123456")
            {
                // Usuario y contraseña correctos
                return RedirectToAction("Index","Alumno");
            }
            else
            {
                // Usuario o contraseña incorrectos
                ViewBag.Error = "Usuario o contraseña incorrectos";
                return View("IniciarSesion");
            }
        }


    }
}