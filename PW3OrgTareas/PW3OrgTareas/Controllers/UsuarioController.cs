using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PW3OrgTareas.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        public ActionResult Index()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                return View();
            }
            else
            {
                Session["RedireccionLogin"] = "Home/Index";
                return RedirectToAction("Login", "Home");
            }
        }

    }
}