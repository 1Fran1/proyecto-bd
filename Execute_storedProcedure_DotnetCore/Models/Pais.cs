using System.ComponentModel.DataAnnotations;

namespace Execute_storedProcedure_DotnetCore.Models
{
    //public class Pais
    //{
    //    [Key]
    //    public int IdPais { get; set; }

    //    [Required(ErrorMessage = "El nombre del país es obligatorio.")]
    //    [MaxLength(100, ErrorMessage = "El nombre del país no puede tener más de 100 caracteres.")]
    //    public string Nombre { get; set; }


    //    public int Status { get; set; }

    //    // public List<Poblacion> Poblaciones { get; set; }  // Si descomentas esta línea, asegúrate de tener una relación correspondiente configurada.
    //}




    public class Pais
    {
        [Key]
        public int IdPais { get; set; }

        public string Nombre { get; set; }


        public int Status { get; set; }

    }


}
