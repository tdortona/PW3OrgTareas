using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PW3OrgTareas.Models
{
    public class Tarea
    {
        public int IdTarea { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaFin { get; set; }

        public string Nombre { get; set; }

        public string Descripcion{ get; set; }

        public string Prioridad { get; set; }

        public Carpeta Carpeta { get; set; }

        public int Estimado { get; set; }

        public bool Completada { get; set; }
    }
}