using System.ComponentModel.DataAnnotations;
using PW3OrgTareas.Metadata.Tarea;

namespace PW3OrgTareas
{
    [MetadataType(typeof(TareaMetaData))]
    public partial class Tarea
    {
        public string NombreCarpeta { get; set; }
    }
}