using System.Collections.Generic;
using PW3OrgTareas.Repository;

namespace PW3OrgTareas.Service
{
    public class CarpetaService
    {
        private readonly CarpetaRepository carpetaRepository = new CarpetaRepository();

        public List<Carpeta> ListarCarpetas()
        {
            return carpetaRepository.ListarCarpetas();
        }

        public List<Carpeta> GetCarpetasByUsuario(int idUsuario)
        {
            return carpetaRepository.GetCarpetasByUsuario(idUsuario);
        }

        public void AgregarCarpeta(Carpeta carpetaNueva, int idUsuario)
        {
            carpetaRepository.AgregarCarpeta(carpetaNueva, idUsuario);
        }

        public Carpeta GetCarpetaById(int idCarpeta)
        {
            return carpetaRepository.GetCarpetaById(idCarpeta);
        }

        public void EliminarCarpeta(int idCarpeta)
        {
            carpetaRepository.EliminarCarpeta(idCarpeta);
        }
    }
}