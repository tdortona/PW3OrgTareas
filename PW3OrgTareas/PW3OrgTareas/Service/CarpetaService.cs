using System.Collections.Generic;
using PW3OrgTareas.Repository;

namespace PW3OrgTareas.Service
{
    public class CarpetaService
    {
        private readonly CarpetaRepository _carpetaRepository = new CarpetaRepository();

        public List<Carpeta> ListarCarpetas()
        {
            return _carpetaRepository.ListarCarpetas();
        }

        public List<Carpeta> GetCarpetasByUsuario(int idUsuario)
        {
            return _carpetaRepository.GetCarpetasByUsuario(idUsuario);
        }

        public void AgregarCarpeta(Carpeta carpetaNueva)
        {
            _carpetaRepository.AgregarCarpeta(carpetaNueva);
        }

        public Carpeta GetCarpetaById(int idCarpeta)
        {
            return _carpetaRepository.GetCarpetaById(idCarpeta);
        }

        public void EliminarCarpeta(int idCarpeta)
        {
            _carpetaRepository.EliminarCarpeta(idCarpeta);
        }
    }
}