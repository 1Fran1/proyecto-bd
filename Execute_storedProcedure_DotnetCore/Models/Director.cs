using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Execute_storedProcedure_DotnetCore.Models
{
  


    public class Director
    {
        [Key]
        public int idDirector { get; set; }

        public int UserId{ get; set; }

        public int Status { get; set; }

    }

}
