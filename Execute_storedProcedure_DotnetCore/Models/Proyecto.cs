using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Globalization;

namespace Execute_storedProcedure_DotnetCore.Models
{
    //public class Proyecto
    //{
    //    [Key]
    //    public int IdProyecto { get; set; }

    //    [Required(ErrorMessage = "El título del proyecto es obligatorio.")]
    //    [MaxLength(200, ErrorMessage = "El título del proyecto no puede tener más de 200 caracteres.")]
    //    public string Titulo { get; set; }

    //    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")]
    //    public DateTime FechaInicio { get; set; }

    //    [Required(ErrorMessage = "La fecha de fin es obligatoria.")]
    //    public DateTime FechaFin { get; set; }

    //    [Required(ErrorMessage = "El presupuesto es obligatorio.")]
    //    public int Presupuesto { get; set; }

    //    [Required(ErrorMessage = "El ID del responsable es obligatorio.")]
    //    [ForeignKey(nameof(Responsable))]
    //    public int IdResponsable { get; set; }
    //    public Responsable Responsable { get; set; }

    //    [Required(ErrorMessage = "El ID de la sede es obligatorio.")]
    //    [ForeignKey(nameof(Sede))]
    //    public int IdSede { get; set; }
    //    public Sede Sede { get; set; }

    //    public List<Poblacion> Poblaciones { get; set; }

    //    public int Status { get; set; }
    //}




    public class Proyecto
    {
        [Key]
        public int IdProyecto { get; set; }

        public string Titulo { get; set; }

        public DateTime Fecha_Inicio { get; set; }

        public DateTime Fecha_Fin { get; set; }

        public decimal Presupuesto { get; set; }

        public int IdResponsable { get; set; }

        public int IdSede { get; set; }

        public int Status { get; set; }
    }



}
