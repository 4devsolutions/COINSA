using System.ComponentModel.DataAnnotations;

namespace NominaNF.Models
{
    public class Proyecto
    {
 
        public int ClaProyecto { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string NomProyecto { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public int BajaLogica { get; set; }
        public DateTime FechaBajaLogica { get; set; }
        public int ClaUsuarioMod { get; set; }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string NombrePcMod { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

    }
}
