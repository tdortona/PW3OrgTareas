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
            var model = _tareaService.GetTareasByUsuario(1);

            return View(model);
        }

        public ActionResult Crear()
        {
            ViewBag.CarpetasUsuario = _carpetaService.GetCarpetasByUsuario(1);

            return View();
        }

        [HttpPost]
        public ActionResult Crear(Tarea tareaNueva)
        {
            _tareaService.AgregarTarea(tareaNueva);

            return RedirectToAction("Index");
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