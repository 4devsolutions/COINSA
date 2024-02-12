using System.ComponentModel.DataAnnotations;

namespace NominaNF.Models
{
    public class Ubicacion
    {

        public int AccionSp { get; set; }
        public int ClaUbicacion { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public string? NomUbicacion { get; set; }
        public int BajaLogica { get; set; }
        public int ClaUsuarioMod { get; set; }
        public string? NombrePcMod { get; set; }

    }
}
