using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW3OrgTareas.Service;

namespace PW3OrgTareas.Controllers
{
    public class CarpetaController : Controller
    {
        private readonly CarpetaService carpetaService = new CarpetaService();

        // GET: Carpeta
        public ActionResult Index()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                return View(carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario));
            }
            Session["RedireccionLogin"] = "Carpeta/Index";
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Crear()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;

            if (usuarioLogueado != null)
            {
                return View();
            }
            else
            {
                Session["RedireccionLogin"] = "Carpeta/Crear";
                return RedirectToAction("Login", "Home");
            }

        }

        [HttpPost]
        public ActionResult Crear(Carpeta carpeta)
        {
            var isValid = ModelState.IsValid;
            var usuarioLogueado = Session["Usuario"] as Usuario;

            if (isValid)
            {
                if (usuarioLogueado != null)
                {
                    carpetaService.AgregarCarpeta(carpeta, usuarioLogueado.IdUsuario);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            return View();
        }

        public ActionResult Tareas(int idCarpeta)
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                return View();
            }
            else
            {
                Session["RedireccionLogin"] = "Carpeta/Tareas";
                return RedirectToAction("Login", "Home");
            }
        }
    }
}