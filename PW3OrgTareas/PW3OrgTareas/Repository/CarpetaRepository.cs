using System;
using System.Collections.Generic;
using System.Linq;

namespace PW3OrgTareas.Repository
{
    public class CarpetaRepository
    {
        private static List<Carpeta> carpetasList = new List<Carpeta>
        {
            new Carpeta
            {
                Nombre = "Carpeta 1",
                Descripcion = "Esta es la primer carpeta de prueba",
                FechaCreacion = DateTime.Today,
                IdCarpeta = 1,
                IdUsuario = 1
            },
            new Carpeta
            {
                Nombre = "Carpeta 2",
                Descripcion = "Esta es la segunda carpeta de prueba",
                FechaCreacion = DateTime.Today,
                IdCarpeta = 2,
                IdUsuario = 1
            },
            new Carpeta
            {
                Nombre = "Carpeta 3",
                Descripcion = "Esta es la tercera carpeta de prueba",
                FechaCreacion = DateTime.Today,
                IdCarpeta = 3,
                IdUsuario = 2
            }
        };

        private readonly TotenEntities ctx = new TotenEntities();

        public List<Carpeta> ListarCarpetas()
        {
            return carpetasList;
        }

        public List<Carpeta> GetCarpetasByUsuario(int idUsuario)
        {
            return ctx.Carpeta.Where(x => x.IdUsuario == idUsuario).OrderBy(x => x.Nombre).ToList();
        }

        public void AgregarCarpeta(Carpeta carpetaNueva)
        {
            carpetaNueva.IdUsuario = 1;
            carpetaNueva.FechaCreacion = DateTime.Now;
            ctx.Carpeta.Add(carpetaNueva);
            ctx.SaveChanges();
        }

        public Carpeta GetCarpetaById(int idCarpeta)
        {
            return carpetasList.FirstOrDefault(x => x.IdCarpeta == idCarpeta);
        }

        public void EliminarCarpeta(int idCarpeta)
        {
            Carpeta carpetaAEliminar;

            if (carpetasList.Any(x => x.IdCarpeta == idCarpeta))
            {
                carpetaAEliminar = carpetasList.FirstOrDefault(x => x.IdCarpeta == idCarpeta);

                carpetasList.Remove(carpetaAEliminar);
            }
        }
    }
}