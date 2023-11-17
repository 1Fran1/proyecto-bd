using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Execute_storedProcedure_DotnetCore.Models
{


    public class Sede
    {
        [Key]
        public int IdSede { get; set; }
        public string Ciudad { get; set; }
        public int Fondo_Presupuestario { get; set; }
        public string Direccion { get; set; }
        public int IdPais { get; set; }
        public int Telefono { get; set; }
        public int IdDirector { get; set; }
        public int Status { get; set; }
    }





}
