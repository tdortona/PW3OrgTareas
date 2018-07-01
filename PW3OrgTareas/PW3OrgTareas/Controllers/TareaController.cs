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

                ViewBag.CarpetasUsuario = carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario);

                foreach (var item in model)
                {
                    Carpeta carpetaDeLaTarea = carpetaService.GetCarpetaById(item.IdCarpeta);
                    item.NombreCarpeta = carpetaDeLaTarea != null ? carpetaDeLaTarea.Nombre : string.Empty;
                }

                return View(model);
            }
            
            Session["RedireccionLogin"] = "Tarea/Index";
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

            Session["RedireccionLogin"] = "Tarea/Crear";
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

        public ActionResult Detalle(int id)
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;

            if (usuarioLogueado != null)
            {
                Tarea model = tareaService.GetTareaByIdConComentariosYAdjuntos(id);

                return View(model);
            }
            else
            {
                Session["RedireccionLogin"] = "Tarea/Index";
                return RedirectToAction("Login", "Home");
            }

        }

        public ActionResult TareasEnCarpeta(int id)
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;

            if (usuarioLogueado != null)
            {
                ViewBag.NombreCarpeta = carpetaService.GetCarpetaById(id).Nombre;
                return View(tareaService.GetTareasByCarpeta(id));
            }
            else
            {
                Session["RedireccionLogin"] = "Tarea/Index/";
                return RedirectToAction("Login", "Home");
            }
        }

        public void CompletarTarea(int idTarea)
        {
            Tarea tareaACompletar = tareaService.GetTareaById(idTarea);
            tareaService.CompletarTarea(tareaACompletar);
        }

        public ActionResult NuevoComentario(int id)
        {
            ComentarioTarea model = new ComentarioTarea()
            {
                IdTarea = id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult NuevoComentario(ComentarioTarea comentario)
        {
            tareaService.CrearComentario(comentario);
            return RedirectToAction("Detalle", new { id = comentario.IdTarea });
        }

        public ActionResult AdjuntarArchivo(int id)
        {
            ArchivoTarea model = new ArchivoTarea()
            {
                IdTarea = id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult AdjuntarArchivo(ArchivoTarea archivo)
        {
            if (Request.Files.Count > 0 && Request.Files[0].ContentLength > 0)
            {
                archivo.RutaArchivo = tareaService.GuardarArchivo(Request.Files[0], Request.Files[0].FileName, archivo.IdTarea);
                tareaService.AdjuntarArchivo(archivo);
            }

            return RedirectToAction("Detalle", new { id = archivo.IdTarea });
        }

        public ActionResult DescargarArchivo(int idTarea, string nombre)
        {
            string carpetaAdjuntos = System.Configuration.ConfigurationManager.AppSettings["CarpetaAdjuntos"];
            carpetaAdjuntos = carpetaAdjuntos.Replace("{idTarea}", idTarea.ToString());
            carpetaAdjuntos = string.Format("/{0}/", carpetaAdjuntos.TrimStart('/').TrimEnd('/'));
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaAdjuntos;

            string path = pathDestino;
            byte[] fileBytes = System.IO.File.ReadAllBytes(path + nombre);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, nombre);
        }
    }
}