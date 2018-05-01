using System;

namespace PW3OrgTareas.Models
{
    public class Carpeta
    {
        public int IdCarpeta { get; set; }

        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}