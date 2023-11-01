using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Execute_storedProcedure_DotnetCore.Models
{
    //public class Sede
    //{
    //    [Key]
    //    public int IdSede { get; set; }

    //    [Required(ErrorMessage = "El ID del país es obligatorio.")]
    //    [ForeignKey(nameof(Pais))]
    //    public int IdPais { get; set; }
    //    public Pais Pais { get; set; }

    //    [Required(ErrorMessage = "El ID del director es obligatorio.")]
    //    [ForeignKey(nameof(Director))]
    //    public int IdDirector { get; set; }
    //    public Director Director { get; set; }

    //    [Required(ErrorMessage = "La ciudad es obligatoria.")]
    //    [MaxLength(100, ErrorMessage = "La ciudad no puede tener más de 100 caracteres.")]
    //    public string Ciudad { get; set; }

    //    [Required(ErrorMessage = "La dirección es obligatoria.")]
    //    [MaxLength(500, ErrorMessage = "La dirección no puede tener más de 500 caracteres.")]
    //    public string Direccion { get; set; }

    //    [Required(ErrorMessage = "El teléfono es obligatorio.")]
    //    [MaxLength(20, ErrorMessage = "El teléfono no puede tener más de 20 caracteres.")]
    //    public string Telefono { get; set; }

    //    public List<Proyecto> Proyectos { get; set; }

    //    public int Status { get; set; }
    //}



    public class Sede
    {
        [Key]
        public int IdSede { get; set; }

        public string Ciudad { get; set; }

        public string Direccion { get; set; }

        public int IdPais { get; set; }

        public int Telefono { get; set; }

        //tener en cuenta que esta variable es una fk para director
        public int IdDirector { get; set; }


        public int Status { get; set; }
    }



}
