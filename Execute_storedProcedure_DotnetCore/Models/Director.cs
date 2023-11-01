using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Execute_storedProcedure_DotnetCore.Models
{
    //public class Director
    //{
    //    [Key]
    //    public int IdDirector { get; set; }

    //    [Required(ErrorMessage = "La identificación es obligatoria.")]
    //    public int Identificacion { get; set; }

    //    [Required(ErrorMessage = "El nombre es obligatorio.")]
    //    [MaxLength(100, ErrorMessage = "El nombre no puede tener más de 100 caracteres.")]
    //    public string Nombre { get; set; }

    //    [Required(ErrorMessage = "El primer apellido es obligatorio.")]
    //    [MaxLength(100, ErrorMessage = "El primer apellido no puede tener más de 100 caracteres.")]
    //    public string Apellido1 { get; set; }

    //    [Required(ErrorMessage = "El segundo apellido es obligatorio.")]
    //    [MaxLength(100, ErrorMessage = "El segundo apellido no puede tener más de 100 caracteres.")]
    //    public string Apellido2 { get; set; }

    //    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    //    public int Telefono { get; set; }

    //    public int Status { get; set; }
    //}

    public class Director
    {
        [Key]
        public int idDirector { get; set; }

        public string identificacion { get; set; }

        public string nombre { get; set; }

        public string apellido1 { get; set; }

        public string apellido2 { get; set; }

        public int telefono { get; set; }

        public int status { get; set; }
    }

}
