using Execute_storedProcedure_DotnetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Execute_storedProcedure_DotnetCore.Data
{
    public class ResponsableContext : DbContext
    {
        public ResponsableContext(DbContextOptions<ResponsableContext> options) : base(options)
        {
        }
        public virtual DbSet<Poblacion> Poblacion { get; set; }
        public virtual DbSet<Actuacion> Actuacion { get; set; }
        public virtual DbSet<PoblacionActuacion> PoblacionActuacion { get; set; }

    }
}
