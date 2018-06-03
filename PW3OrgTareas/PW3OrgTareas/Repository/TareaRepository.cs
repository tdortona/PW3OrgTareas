using System;
using System.Collections.Generic;
using System.Linq;
using PW3OrgTareas.Enums;

namespace PW3OrgTareas.Repository
{
    public class TareaRepository
    {
        private static List<Tarea> tareasList = new List<Tarea>
        {
            new Tarea
            {
                Nombre = "Tarea 1",
                Descripcion = "Esta es la primer tarea de prueba",
                FechaCreacion = DateTime.Today,
                FechaFin = DateTime.Today.AddYears(1),
                EstimadoHoras = 8,
                IdCarpeta = 1,
                Prioridad = (int)PrioridadEnum.Baja,
                IdTarea = 1,
                IdUsuario = 1,
                Completada = 0
            },
            new Tarea
            {
                Nombre = "Tarea 2",
                Descripcion = "Esta es la segunda tarea de prueba",
                FechaCreacion = DateTime.Today,
                FechaFin = DateTime.Today.AddYears(1),
                EstimadoHoras = 8,
                IdCarpeta = 1,
                Prioridad = (int)PrioridadEnum.Baja,
                IdTarea = 2,
                IdUsuario = 1,
                Completada = 0
            },
            new Tarea
            {
                Nombre = "Tarea 3",
                Descripcion = "Esta es la tercera tarea de prueba",
                FechaCreacion = DateTime.Today,
                FechaFin = DateTime.Today.AddYears(1),
                EstimadoHoras = 8,
                IdCarpeta = 1,
                Prioridad = (int)PrioridadEnum.Baja,
                IdTarea = 3,
                IdUsuario = 2,
                Completada = 0
            }
        };

        private readonly TotenEntities ctx = new TotenEntities();

        public List<Tarea> ListarTareas()
        {
            return tareasList;
        }

        public List<Tarea> GetTareasByUsuario(int idUsuario)
        {
            return ctx.Tarea.Where(x => x.IdUsuario == idUsuario).OrderByDescending(x => x.FechaCreacion).ToList();
        }

        public List<Tarea> GetTareasNoCompletadasByUsuario(int idUsuario)
        {
            return tareasList.Where(x => x.IdUsuario == idUsuario && x.Completada != 1).OrderBy(x => x.Prioridad).ThenBy(x => x.FechaFin).ToList();
        }

        public List<Tarea> GetTareasByCarpeta(int idCarpeta)
        {
            return tareasList.Where(x => x.IdCarpeta == idCarpeta).ToList();
        }

        public void AgregarTarea(Tarea tareaNueva)
        {
            tareaNueva.IdUsuario = 1;
            tareaNueva.Completada = 0;
            tareaNueva.FechaCreacion = DateTime.Now;
            ctx.Tarea.Add(tareaNueva);
            ctx.SaveChanges();
        }

        public Tarea GetTareaById(int idTarea)
        {
            return tareasList.FirstOrDefault(x => x.IdTarea == idTarea);
        }

        public void EliminarTarea(int idTarea)
        {
            Tarea tareaAEliminar;

            if (tareasList.Any(x => x.IdTarea == idTarea))
            {
                tareaAEliminar = tareasList.FirstOrDefault(x => x.IdTarea == idTarea);

                tareasList.Remove(tareaAEliminar);
            }
        }
    }
}