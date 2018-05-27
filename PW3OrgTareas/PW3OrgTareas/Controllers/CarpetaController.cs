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
        private int idUsuario = 1;
        private readonly CarpetaService _carpetaService = new CarpetaService();

        // GET: Carpeta
        public ActionResult Index()
        {
            return View(_carpetaService.GetCarpetasByUsuario(idUsuario));
        }

        public ActionResult Crear()
        {
            return View();
        }

        public ActionResult Tareas(int idCarpeta)
        {
            return View();
        }
    }
}