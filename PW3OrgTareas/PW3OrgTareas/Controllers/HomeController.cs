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

        public ActionResult Index()
        {
            var model = this._tareaService.GetTareasNoCompletadasByUsuario(1);

            ViewBag.CarpetasUsuario = _carpetaService.GetCarpetasByUsuario(1);

            return View(model);
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            return View("Index");
        }

        public ActionResult Registro()
        {
            return View();
        }
    }
}