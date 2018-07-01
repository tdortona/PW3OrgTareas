using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using PW3OrgTareas.Service;

namespace PW3OrgTareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly TareaService tareaService = new TareaService();
        private readonly CarpetaService carpetaService = new CarpetaService();
        private readonly UsuarioService usuarioService = new UsuarioService();

        public ActionResult Index()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                var model = this.tareaService.GetTareasNoCompletadasByUsuario(usuarioLogueado.IdUsuario);

                ViewBag.CarpetasUsuario = carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario);

                foreach (var item in model)
                {
                    Carpeta carpetaDeLaTarea = carpetaService.GetCarpetaById(item.IdCarpeta);
                    item.NombreCarpeta = carpetaDeLaTarea != null ? carpetaDeLaTarea.Nombre : string.Empty;
                }

                return View(model);
            }
            
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            var user = usuarioService.VerificarExistenciaUsuario(u);
            
            if (user != null)
            {
                Session["Usuario"] = user;
                if (Session["RedireccionLogin"] != null)
                {
                    String accionSesion = (String)Session["RedireccionLogin"];
                    String pattern = "/";
                    String[] accion = Regex.Split(accionSesion, pattern);
                    Session.Remove("RedireccionLogin");
                    return RedirectToAction(accion[1], accion[0]);
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return RedirectToAction("Login");
        }

        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registro(Usuario newUser)
        {
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                usuarioService.RegistrarUsuario(newUser);
                return RedirectToAction("Login");
            }
            
            return View();
        }
    }
}