using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using PW3OrgTareas.Repository;

namespace PW3OrgTareas.Service
{
    public class TareaService
    {
        private readonly TareaRepository tareaRepository = new TareaRepository();

        public List<Tarea> ListarTareas()
        {
            return tareaRepository.ListarTareas();
        }

        public List<Tarea> GetTareasByUsuario(int idUsuario)
        {
            return tareaRepository.GetTareasByUsuario(idUsuario);
        }

        public List<Tarea> GetTareasNoCompletadasByUsuario(int idUsuario)
        {
            return tareaRepository.GetTareasNoCompletadasByUsuario(idUsuario);
        }

        public List<Tarea> GetTareasByCarpeta(int idCarpeta, int idUsuario)
        {
            return tareaRepository.GetTareasByCarpeta(idCarpeta, idUsuario);
        }

        public void AgregarTarea(Tarea tareaNueva, int idUsuario)
        {
            tareaRepository.AgregarTarea(tareaNueva, idUsuario);
        }

        public Tarea GetTareaById(int idTarea)
        {
            return tareaRepository.GetTareaById(idTarea);
        }

        public Tarea GetTareaByIdConComentariosYAdjuntos(int idTarea)
        {
            return tareaRepository.GetTareaByIdConComentariosYAdjuntos(idTarea);
        }

        public void EliminarTarea(int idTarea)
        {
            tareaRepository.EliminarTarea(idTarea);
        }

        public void CompletarTarea(Tarea tareaACompletar)
        {
            tareaRepository.CompletarTarea(tareaACompletar);
        }

        public void CrearComentario(ComentarioTarea comentario)
        {
            tareaRepository.CrearComentario(comentario);
        }

        public string GuardarArchivo(HttpPostedFileBase archivoSubido, string nombre, int idTarea)
        {
            string carpetaAdjuntos = System.Configuration.ConfigurationManager.AppSettings["CarpetaAdjuntos"];
            carpetaAdjuntos = carpetaAdjuntos.Replace("{idTarea}", idTarea.ToString());
            carpetaAdjuntos = string.Format("/{0}/", carpetaAdjuntos.TrimStart('/').TrimEnd('/'));
            string pathDestino = System.Web.Hosting.HostingEnvironment.MapPath("~") + carpetaAdjuntos;

            if (!Directory.Exists(pathDestino))
            {
                Directory.CreateDirectory(pathDestino);
            }

            string nombreArchivoFinal = GenerarNombreUnico(nombre);
            nombreArchivoFinal = string.Concat(nombreArchivoFinal, Path.GetExtension(archivoSubido.FileName));
            archivoSubido.SaveAs(string.Concat(pathDestino, nombreArchivoFinal));
            
            return string.Concat(carpetaAdjuntos, nombreArchivoFinal);
        }

        public void AdjuntarArchivo(ArchivoTarea archivo)
        {
            tareaRepository.AdjuntarArchivo(archivo);
        }

        private static string GenerarNombreUnico(string nombre)
        {
            string randomString = System.Web.Security.Membership.GeneratePassword(20, 0);
            Random rnd = new Random();
            
            randomString = Regex.Replace(randomString, @"[^a-zA-Z0-9]", m => "");
            
            nombre = Regex.Replace(nombre.Trim(), @"[^a-zA-Z0-9]", m => "").ToLower();
            
            return string.Format("{0}-{1}", nombre, randomString);
        }
    }
}