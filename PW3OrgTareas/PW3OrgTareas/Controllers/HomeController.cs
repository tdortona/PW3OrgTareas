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
            if (Request.Cookies.AllKeys.Contains("usuarioSesion") && Request.Cookies["usuarioSesion"].Values.Count > 0)
            {
                var cookie = Request.Cookies["usuarioSesion"].Value;

                if (cookie != null && !string.IsNullOrWhiteSpace(cookie))
                {
                    byte[] decryted = Convert.FromBase64String(string.IsNullOrWhiteSpace(cookie) ? string.Empty : cookie);
                    var result = Int32.Parse(System.Text.Encoding.Unicode.GetString(decryted));

                    var usuario = usuarioService.GetById(result);
                    if (usuario != null)
                    {
                        Session["Usuario"] = usuario;
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        return View();
                    }
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Usuario u)
        {
            bool recuerdame = Request.Form["Recordame"] == "on";
            var user = usuarioService.VerificarExistenciaUsuario(u);
            if (user != null)
            {
                if (user.Activo != 0)
                {
                    Session["Usuario"] = user;

                    string result = string.Empty;
                    Usuario usuarioCookie = usuarioService.BuscarUsuarioPorMail(u.Email);

                    if (recuerdame)
                    {
                        byte[] encryted = System.Text.Encoding.Unicode.GetBytes(Convert.ToString(usuarioCookie.IdUsuario));
                        result = Convert.ToBase64String(encryted);
                        Response.Cookies["usuarioSesion"].Value = result;
                    }

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
            Response.Cookies["usuarioSesion"].Value = null;

            return RedirectToAction("Login");
        }

        public ActionResult Registro()
        {
            var usuarioLogueado = Session["Usuario"] as Usuario;
            if (usuarioLogueado != null)
            {
                return RedirectToAction("Index", "Home");
            }
            
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