using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Execute_storedProcedure_DotnetCore.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        //public int id
    }
}
