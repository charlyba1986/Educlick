using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CapaEntidad;
using CapaDatos;

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
            try
            {
                Usuario user = new Usuario();
                user.UsuarioSistema = Usuario;
                UsuarioData usuarioData= new CapaDatos.UsuarioData();
                Usuario usuarioEncontrado= usuarioData.ConsultarUsuario(user);


                if(usuarioEncontrado != null && usuarioEncontrado.Password == Contra)
                {
                    // Usuario y contraseña correctos
                    if(usuarioEncontrado.Rol == "Admin")
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if(usuarioEncontrado.Rol == "Profesor")
                    {
                        return RedirectToAction("Details", "Profesor");
                    }
                    else if(usuarioEncontrado.Rol == "Alumno")
                    {
                        return RedirectToAction("Index", "Alumno");
                    }
                    return RedirectToAction("Index", "Alumno");
                }
                else
                {
                    // Usuario o contraseña incorrectos
                    ViewBag.Error = "Usuario o contraseña incorrectos";
                    return View("IniciarSesion");
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Error al verificar el usuario: " + ex.Message;
                return View("IniciarSesion");
            }

            // Aquí iría la lógica para verificar el usuario y la contraseña
            // Por simplicidad, asumimos que el usuario es "admin" y la contraseña es "password"
            //if (Usuario == "admin@gmail.com" && Contra == "123456")
            //{
            //    Usuario y contraseña correctos
            //    return RedirectToAction("Index", "Alumno");
            //}
            //else
            //{
            //    Usuario o contraseña incorrectos
            //    ViewBag.Error = "Usuario o contraseña incorrectos";
            //    return View("IniciarSesion");
            //}
        }


    }
}