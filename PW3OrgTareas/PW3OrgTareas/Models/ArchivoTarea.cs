using System;

namespace PW3OrgTareas.Models
{
    public class ArchivoTarea
    {
        public int IdArchivoTarea { get; set; }

        public string RutaArchivo { get; set; }

        public int IdTarea { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}