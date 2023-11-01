using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Execute_storedProcedure_DotnetCore.Models
{
    //public class Actuacion
    //{
    //    [Key]
    //    public int IdActuacion { get; set; }

    //    [Required(ErrorMessage = "El presupuesto es obligatorio.")]
    //    public int Presupuesto { get; set; }

    //    [Required(ErrorMessage = "El nombre es obligatorio.")]
    //    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    //    public string Nombre { get; set; }

    //    [Required(ErrorMessage = "La descripción es obligatoria.")]
    //    [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
    //    public string Descripcion { get; set; }

    //    //[Required(ErrorMessage = "El ID del proyecto es obligatorio.")]
    //    [ForeignKey(nameof(Proyecto))]
    //    public int IdProyecto { get; set; }
    //    public Proyecto Proyecto { get; set; }

    //    public List<Poblacion> Poblaciones { get; set; }

    //    public int Status { get; set; }
    //}



    public class Actuacion
    {
        [Key]
        public int IdActuacion { get; set; }

        public decimal Presupuesto { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public int IdProyecto { get; set; }

        public int Status { get; set; }
    }




}
