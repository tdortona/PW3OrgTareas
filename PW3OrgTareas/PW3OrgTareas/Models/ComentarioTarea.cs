using System;

namespace PW3OrgTareas.Models
{
    public class ComentarioTarea
    {
        public int IdComentarioTarea { get; set; }

        public string Texto { get; set; }

        public int IdTarea { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}