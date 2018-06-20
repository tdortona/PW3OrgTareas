using System;
using System.Collections.Generic;
using System.Web.Mvc;
using PW3OrgTareas.Enums;
using PW3OrgTareas.Service;

namespace PW3OrgTareas.Controllers
{
    public class TareaController : Controller
    {
        private readonly TareaService tareaService = new TareaService();
        private readonly CarpetaService carpetaService = new CarpetaService();

        // GET: Tarea
        public ActionResult Index()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                var model = tareaService.GetTareasByUsuario(usuarioLogueado.IdUsuario);

                return View(model);
            }
            
            return RedirectToAction("Login", "Home");
        }

        public ActionResult Crear()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                List<SelectListItem> listaPrioridad = new List<SelectListItem>();
                List<SelectListItem> listaCarpetas = new List<SelectListItem>();
                Carpeta carpetaGeneral = new Carpeta();

                foreach (var item in Enum.GetValues(typeof(PrioridadEnum)))
                {
                    listaPrioridad.Add(new SelectListItem
                    {
                        Value = item.GetHashCode().ToString(),
                        Text = item.ToString()
                    });
                }

                foreach (var item in carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario))
                {
                    listaCarpetas.Add(new SelectListItem
                    {
                        Value = item.IdCarpeta.ToString(),
                        Text = item.Nombre
                    });
                }

                carpetaGeneral = carpetaService.GetCarpetaById(1);  // Obtengo la carpeta general
                listaCarpetas.Insert(0, new SelectListItem
                {
                    Value = carpetaGeneral.IdCarpeta.ToString(),
                    Text = carpetaGeneral.Nombre
                });

                ViewBag.ListaPrioridad = listaPrioridad;
                ViewBag.CarpetasUsuario = listaCarpetas;

                return View();
            }

            return RedirectToAction("Login", "Home");
        }

        [HttpPost]
        public ActionResult Crear(Tarea tareaNueva)
        {
            var isValid = ModelState.IsValid;
            var usuarioLogueado = Session["Usuario"] as Usuario;

            if (isValid)
            {
                if (usuarioLogueado != null)
                {
                    tareaService.AgregarTarea(tareaNueva, usuarioLogueado.IdUsuario);
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }

            List<SelectListItem> listaPrioridad = new List<SelectListItem>();
            List<SelectListItem> listaCarpetas = new List<SelectListItem>();
            Carpeta carpetaGeneral = new Carpeta();

            foreach (var item in Enum.GetValues(typeof(PrioridadEnum)))
            {
                listaPrioridad.Add(new SelectListItem
                {
                    Value = item.GetHashCode().ToString(),
                    Text = item.ToString()
                });
            }

            foreach (var item in carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario))
            {
                listaCarpetas.Add(new SelectListItem
                {
                    Value = item.IdCarpeta.ToString(),
                    Text = item.Nombre
                });
            }

            carpetaGeneral = carpetaService.GetCarpetaById(1);  // Obtengo la carpeta general
            listaCarpetas.Insert(0, new SelectListItem
            {
                Value = carpetaGeneral.IdCarpeta.ToString(),
                Text = carpetaGeneral.Nombre
            });

            ViewBag.ListaPrioridad = listaPrioridad;
            ViewBag.CarpetasUsuario = listaCarpetas;

            return View();
        }

        public ActionResult Detalle(int idTarea)
        {
            return View();
        }

        public ActionResult TareasEnCarpeta(int id)
        {
            ViewBag.NombreCarpeta = carpetaService.GetCarpetaById(id).Nombre;

            return View(tareaService.GetTareasByCarpeta(id));
        }

        public void CompletarTarea(int idTarea)
        {
            Tarea tareaACompletar = tareaService.GetTareaById(idTarea);
            tareaService.CompletarTarea(tareaACompletar);
        }
    }
}