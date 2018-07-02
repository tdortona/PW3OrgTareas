using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Mvc;
using Newtonsoft.Json.Linq;
using PW3OrgTareas.Service;

namespace PW3OrgTareas.Controllers
{
    public class HomeController : Controller
    {
        private readonly TareaService tareaService = new TareaService();
        private readonly CarpetaService carpetaService = new CarpetaService();
        private readonly UsuarioService usuarioService = new UsuarioService();

        public ActionResult Index()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                var model = this.tareaService.GetTareasNoCompletadasByUsuario(usuarioLogueado.IdUsuario);

                ViewBag.CarpetasUsuario = carpetaService.GetCarpetasByUsuario(usuarioLogueado.IdUsuario);

                foreach (var item in model)
                {
                    Carpeta carpetaDeLaTarea = carpetaService.GetCarpetaById(item.IdCarpeta);
                    item.NombreCarpeta = carpetaDeLaTarea != null ? carpetaDeLaTarea.Nombre : string.Empty;
                }

                return View(model);
            }

            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            var user = usuarioService.VerificarExistenciaUsuario(u);
            if (user != null)
            {
                if (user.Activo != 0)
                {
                    Session["Usuario"] = user;
                    if (Session["RedireccionLogin"] != null)
                    {
                        String accionSesion = (String)Session["RedireccionLogin"];
                        String pattern = "/";
                        String[] accion = Regex.Split(accionSesion, pattern);
                        Session.Remove("RedireccionLogin");
                        return RedirectToAction(accion[1], accion[0]);
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.MensajeDeError = "Su usuario se encuentra inactivo";
                    return View();
                }
            }
            else
            {
                ViewBag.MensajeDeError = "Verifique por favor su Usuario y/o Contraseña";
                return View();
            }
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
            bool x = newUser.Password.Any(p => char.IsUpper(p));
            bool y = newUser.Password.Any(p => char.IsLower(p));
            bool z = newUser.Password.Any(p => char.IsDigit(p));

            var isValid = ModelState.IsValid;

            var response = Request["g-recaptcha-response"];
            string secretKey = "6Ld3NWAUAAAAAJJ2Mco-UNBPCdXwIZwIeNs1r6fC";
            var client = new WebClient();
            var result = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(result);
            var status = (bool)obj.SelectToken("success");
            ViewBag.Message = status ? "" : "Por favor, marque la casilla del captcha";

            if (status == true)
            {
                if (isValid)
                {
                    if (x && y && z)
                    {
                        if (usuarioService.BuscarUsuarioPorMail(newUser.Email) != null)
                        {
                            if (usuarioService.VerificarUsuarioActivo(newUser))
                            {
                                newUser.CodigoActivacion = result;
                                return RedirectToAction("Login");
                            }
                            else
                            {
                                ViewBag.MensajeEmailExistente = "El email ingresado ya se encuentra en uso.";
                                return View(newUser);
                            }
                        }
                        usuarioService.RegistrarUsuario(newUser);
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.mensajeDeErrorPassword = "La contraseña debe contener mayusculas, minusculas y numeros.";
                        return View(newUser);
                    }
                }
            }

            return View(newUser);
        }
    }
}