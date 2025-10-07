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

        //public IActtionResult IniciarSesion(string email, string password)
        //{
        //    var  usuario = _usuariosService.Autenticar(email, password);

        //    if (usuario == null)
        //    {

        //        HttpContext.Session.SetString("Rol", usuario.Rol);
        //    }
        //}
    }
}