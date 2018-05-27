using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PW3OrgTareas.Enums;
using PW3OrgTareas.Models;

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
                IdUsuario = 1,
                Completada = 0
            }
        };

        public List<Tarea> ListarTareas()
        {
            return tareasList;
        }

        public List<Tarea> GetTareasByUsuario(int idUsuario)
        {
            return tareasList.Where(x => x.IdUsuario == idUsuario).ToList();
        }

        public List<Tarea> GetTareasByCarpeta(int idCarpeta)
        {
            return tareasList.Where(x => x.IdCarpeta == idCarpeta).ToList();
        }

        public void AgregarTarea(Tarea tareaNueva)
        {
            if (!tareasList.Any(x => x == tareaNueva))
            {
                tareasList.Add(tareaNueva);
            }
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