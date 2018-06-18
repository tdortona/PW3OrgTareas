using System.ComponentModel.DataAnnotations;

namespace PW3OrgTareas.Metadata.Carpeta
{
    public class CarpetaMetaData
    {
        [Required(ErrorMessage = "El nombre de la carpeta es obligatorio.")]
        public string Nombre { get; set; }
    }
}