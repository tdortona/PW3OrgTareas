using System.ComponentModel.DataAnnotations;

namespace PW3OrgTareas.Metadata.Tarea
{
    public class TareaMetaData
    {
        [Required(ErrorMessage = "El nombre de la tarea es obligatorio.")]
        public string Nombre { get; set; }
    }
}