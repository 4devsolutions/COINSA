using NominaNF.Models;

namespace NominaNF.ViewModels
{
    public class UsuarioViewModel
    {
        public List<Proyecto> Proyectos { get; set; }

        // Add other properties from the Usuario model as needed
        public int ClaUsuario { get; set; }
        public string Nombre { get; set; }
        public string ApellidoPaterno { get; set; }
        // Include other properties as needed
    }
}
