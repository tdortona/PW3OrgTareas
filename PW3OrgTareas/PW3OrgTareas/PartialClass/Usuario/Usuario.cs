using System.ComponentModel.DataAnnotations;
using PW3OrgTareas.Metadata.Usuario;

namespace PW3OrgTareas
{
    [MetadataType(typeof(UsuarioMetaData))]
    public partial class Usuario
    {
        public string RepetirPassword { get; set; }
    }
}