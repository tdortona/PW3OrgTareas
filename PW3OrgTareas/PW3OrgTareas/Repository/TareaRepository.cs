using System;
using System.Collections.Generic;
using System.Linq;
using PW3OrgTareas.Enums;

namespace PW3OrgTareas.Repository
{
    public class TareaRepository
    {
        private readonly TotenEntities ctx = new TotenEntities();

        public List<Tarea> ListarTareas()
        {
            return ctx.Tarea.ToList();
        }

        public List<Tarea> GetTareasByUsuario(int idUsuario)
        {
            return ctx.Tarea.Where(x => x.IdUsuario == idUsuario).OrderByDescending(x => x.FechaCreacion).ToList();
        }

        public List<Tarea> GetTareasNoCompletadasByUsuario(int idUsuario)
        {
            return ctx.Tarea.Where(x => x.IdUsuario == idUsuario && x.Completada != 1).OrderBy(x => x.Prioridad).ThenBy(x => x.FechaFin).ToList();
        }

        public List<Tarea> GetTareasByCarpeta(int idCarpeta)
        {
            return ctx.Tarea.Where(x => x.IdCarpeta == idCarpeta).ToList();
        }

        public void AgregarTarea(Tarea tareaNueva, int idUsuario)
        {
            tareaNueva.IdCarpeta = tareaNueva.IdCarpeta;
            tareaNueva.IdUsuario = idUsuario;
            tareaNueva.Completada = 0;
            tareaNueva.FechaCreacion = DateTime.Now;
            ctx.Tarea.Add(tareaNueva);
            ctx.SaveChanges();
        }

        public Tarea GetTareaById(int idTarea)
        {
            return ctx.Tarea.FirstOrDefault(x => x.IdTarea == idTarea);
        }

        public void EliminarTarea(int idTarea)
        {
            Tarea tareaAEliminar;

            if (ctx.Tarea.Any(x => x.IdTarea == idTarea))
            {
                tareaAEliminar = ctx.Tarea.FirstOrDefault(x => x.IdTarea == idTarea);

                ctx.Tarea.Remove(tareaAEliminar);
                ctx.SaveChanges();
            }
        }

        public void CompletarTarea(Tarea tareaACompletar)
        {
            tareaACompletar.Completada = 1;
            ctx.SaveChanges();
        }
    }
}