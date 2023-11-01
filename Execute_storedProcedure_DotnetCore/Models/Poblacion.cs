using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Execute_storedProcedure_DotnetCore.Models
{
    //public class Poblacion
    //{
    //    [Key]
    //    public int IdPoblacion { get; set; }

    //    [Required(ErrorMessage = "El nombre de la población es obligatorio.")]
    //    [MaxLength(100, ErrorMessage = "El nombre de la población no puede tener más de 100 caracteres.")]
    //    public string Nombre { get; set; }

    //    [Required(ErrorMessage = "El pais de la población es obligatorio.")]
    //    [ForeignKey(nameof(Pais))]
    //    public int IdPais { get; set; }
    //    public Pais Pais { get; set; }

    //    [Required(ErrorMessage = "El número de habitantes es obligatorio.")]
    //    public int NumHabitantes { get; set; }

    //    [Required(ErrorMessage = "La descripción es obligatoria.")]
    //    [MaxLength(500, ErrorMessage = "La descripción no puede tener más de 500 caracteres.")]
    //    public string Descripcion { get; set; }

    //    [Required(ErrorMessage = "El ID de la actuación es obligatorio.")]
    //    [ForeignKey(nameof(Actuacion))]
    //    public int IdActuacion { get; set; }
    //    public Actuacion Actuacion { get; set; }

    //    public int Status { get; set; }
    //}






    public class Poblacion
    {
        [Key]
        public int IdPoblacion { get; set; }

        public string Nombre { get; set; }

        public int IdPais { get; set; }

        public int NumHabitantes { get; set; }

        public string Descripcion { get; set; }

        public int IdActuacion { get; set; }

        public int Status { get; set; }
    }


}
