using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW3OrgTareas.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}