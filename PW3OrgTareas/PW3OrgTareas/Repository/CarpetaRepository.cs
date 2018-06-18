using System;
using System.Collections.Generic;
using System.Linq;

namespace PW3OrgTareas.Repository
{
    public class CarpetaRepository
    {
        private readonly TotenEntities ctx = new TotenEntities();

        public List<Carpeta> ListarCarpetas()
        {
            return ctx.Carpeta.ToList();
        }

        public List<Carpeta> GetCarpetasByUsuario(int idUsuario)
        {
            return ctx.Carpeta.Where(x => x.IdUsuario == idUsuario).OrderBy(x => x.Nombre).ToList();
        }

        public void AgregarCarpeta(Carpeta carpetaNueva, int idUsuario)
        {
            carpetaNueva.IdUsuario = idUsuario;
            carpetaNueva.FechaCreacion = DateTime.Now;
            ctx.Carpeta.Add(carpetaNueva);
            ctx.SaveChanges();
        }

        public Carpeta GetCarpetaById(int idCarpeta)
        {
            return ctx.Carpeta.FirstOrDefault(x => x.IdCarpeta == idCarpeta);
        }

        public void EliminarCarpeta(int idCarpeta)
        {
            Carpeta carpetaAEliminar;

            if (ctx.Carpeta.Any(x => x.IdCarpeta == idCarpeta))
            {
                carpetaAEliminar = ctx.Carpeta.FirstOrDefault(x => x.IdCarpeta == idCarpeta);

                ctx.Carpeta.Remove(carpetaAEliminar);
                ctx.SaveChanges();
            }
        }
    }
}