﻿using System.ComponentModel.DataAnnotations;

namespace NominaNF.Models
{
    public class Proyecto
    {
        public int AccionSp { get; set; }
        public int ClaProyecto { get; set; }
        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        public required string NomProyecto { get; set; }
        public int BajaLogica { get; set; }
        public int ClaUsuarioMod { get; set; }
        public string? NombrePcMod { get; set; }


    }
}
