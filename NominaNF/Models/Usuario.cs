using System.ComponentModel.DataAnnotations;

namespace NominaNF.Models
{
    public class Usuario
    {

        public int AccionSp { get; set; }
        public int ClaUsuario { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? Nombre { get; set; }
        [Required(ErrorMessage = "El campo Apellido Paterno es obligatorio")]
        public string? ApellidoPaterno { get; set; }
        public string? ApellidoMaterno { get; set; }
        public int ClaPuesto { get; set; }
        public string? NomPuesto { get; set; }
        public int ClaProyecto { get; set; }
        public string? NomProyecto { get; set; }
        public int ClaUbicacion { get; set; }
        public string? NomUbicacion { get; set; }
        public int BajaLogica { get; set; }
        public int ClaUsuarioMod { get; set; }
        public string? NombrePcMod { get; set; }

    }
}
