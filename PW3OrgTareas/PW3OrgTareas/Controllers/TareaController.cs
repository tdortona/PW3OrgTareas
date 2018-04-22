using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW3OrgTareas.Controllers
{
    public class TareaController : Controller
    {
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
    }
}