using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW3OrgTareas.Service;

namespace PW3OrgTareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly TareaService _tareaService = new TareaService();
        private readonly CarpetaService _carpetaService = new CarpetaService();
        private readonly UsuarioService _usuarioService = new UsuarioService();

        public ActionResult Index()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                var model = this._tareaService.GetTareasNoCompletadasByUsuario(usuarioLogueado.IdUsuario);

                ViewBag.CarpetasUsuario = _carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario);

                return View(model);
            }

            return RedirectToAction("Login", "Home");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            var user = _usuarioService.VerificarExistenciaUsuario(u);
            var isValid = ModelState.IsValid;

            if (isValid)
            {
                if (user != null)
                {
                    Session["Usuario"] = user;
                    return RedirectToAction("Index", "Home");
                }
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
            _usuarioService.RegistrarUsuario(newUser);
            return RedirectToAction("Login");
        }
    }
}