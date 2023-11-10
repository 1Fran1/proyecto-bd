using Execute_storedProcedure_DotnetCore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Execute_storedProcedure_DotnetCore.Data
{



    public class MiApiContext : IdentityDbContext<ApplicationUser>
    {
       public MiApiContext(DbContextOptions<MiApiContext> options) : base(options)
    {

    }

        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Responsable> Responsable { get; set; }
        public virtual DbSet<Sede> Sede { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }
        public virtual DbSet<Poblacion> Poblacion { get; set; }
        public virtual DbSet<Actuacion> Actuacion { get; set; }
        public virtual DbSet<PoblacionActuacion> PoblacionActuacion { get; set; }






}
}