using System;

namespace PW3OrgTareas.Models
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int Activo { get; set; }

        public DateTime FechaRegistracion { get; set; }

        public DateTime FechaActivacion { get; set; }

        public string CodigoActivacion { get; set; }
    }
}