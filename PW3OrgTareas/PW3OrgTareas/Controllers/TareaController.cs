using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PW3OrgTareas.Service;

namespace PW3OrgTareas.Controllers
{
    public class TareaController : Controller
    {
        private readonly TareaService _tareaService = new TareaService();
        private readonly CarpetaService _carpetaService = new CarpetaService();

        // GET: Tarea
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Detalle(int idTarea)
        {
            return View();
        }

        public ActionResult TareasEnCarpeta(int id)
        {
            ViewBag.NombreCarpeta = _carpetaService.GetCarpetaById(id).Nombre;

            return View(_tareaService.GetTareasByCarpeta(id));
        }
    }
}