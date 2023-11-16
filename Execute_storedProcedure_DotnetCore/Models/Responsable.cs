using System.ComponentModel.DataAnnotations;

namespace Execute_storedProcedure_DotnetCore.Models
{


    public class Responsable
    {
        [Key]
        public int IdResponsable { get; set; }

        public int UserId { get; set; }

        public int Status { get; set; }
    }


}
