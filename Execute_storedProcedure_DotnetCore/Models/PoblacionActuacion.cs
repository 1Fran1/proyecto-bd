using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Execute_storedProcedure_DotnetCore.Models
{
    public class PoblacionActuacion
    {
        [Key]
        public int IdPoblacion_Actuacion { get; set; }

        [Required]
        public int idPoblacion { get; set; }

        [Required]
        public int idActuacion { get; set; }

    }
}
