using System;

namespace PW3OrgTareas.Models
{
    public class Tarea
    {
        public int IdTarea { get; set; }

        public int IdUsuario { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaFin { get; set; }

        public string Nombre { get; set; }

        public string Descripcion{ get; set; }

        public int Prioridad { get; set; }

        public Carpeta Carpeta { get; set; }

        public decimal EstimadoHoras { get; set; }

        public int Completada { get; set; }
    }
}