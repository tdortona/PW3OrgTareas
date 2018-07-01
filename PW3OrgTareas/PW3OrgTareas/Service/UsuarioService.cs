using PW3OrgTareas.Repository;

namespace PW3OrgTareas.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository usuarioRepository = new UsuarioRepository();

        public Usuario VerificarExistenciaUsuario(Usuario u)
        {
            return usuarioRepository.VerificarExistenciaUsuario(u);
        }

        public void RegistrarUsuario(Usuario u)
        {
            usuarioRepository.RegistrarUsuario(u);
        }

        public Usuario BuscarUsuarioPorMail(string email)
        {
            return usuarioRepository.BuscarUsuarioPorMail(email);
        }

        public bool VerificarUsuarioActivo(Usuario u)
        {
            Usuario usuarioAVerificar = BuscarUsuarioPorMail(u.Email);
            if (usuarioAVerificar.Activo == 1)
            {
                return false;
            }
            else
            {
                u.Activo = 1;
                usuarioRepository.ModificarUsuario(u);
                return true;
            }
        }
    }
}