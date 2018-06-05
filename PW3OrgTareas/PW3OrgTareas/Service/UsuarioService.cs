using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PW3OrgTareas.Repository;

namespace PW3OrgTareas.Service
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository = new UsuarioRepository();

        public Usuario VerificarExistenciaUsuario(Usuario u)
        {
            return _usuarioRepository.VerificarExistenciaUsuario(u);
        }

        public void RegistrarUsuario(Usuario u)
        {
            _usuarioRepository.RegistrarUsuario(u);
        }
    }
}