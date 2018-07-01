using System.ComponentModel.DataAnnotations;

namespace PW3OrgTareas.Metadata.Usuario
{
    public class UsuarioMetaData
    {
        [Required(ErrorMessage = "Tu nombre es obligatorio.")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Tu apellido es obligatorio.")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Tu email es obligatorio.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Tu contraseña es obligatoria.")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Las contraseñas deben coincidir.")]
        public string RepetirPassword { get; set; }
    }
}