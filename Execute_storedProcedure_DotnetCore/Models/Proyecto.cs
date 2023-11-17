using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;
using System.Globalization;

namespace Execute_storedProcedure_DotnetCore.Models
{



    public class Proyecto
    {
        [Key]
        public int IdProyecto { get; set; }
        public string Titulo { get; set; }
        public DateTime Fecha_Inicio { get; set; }
        public DateTime Fecha_Fin { get; set; }
        public int Presupuesto { get; set; }
        public int IdResponsable { get; set; }
        public int IdSede { get; set; }
        public int Status { get; set; }
    }





}
