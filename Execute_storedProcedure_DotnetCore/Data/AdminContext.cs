using Execute_storedProcedure_DotnetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Execute_storedProcedure_DotnetCore.Data
{
    public class AdminContext : DbContext
    {
        public AdminContext(DbContextOptions<AdminContext> options) : base(options)
        {
        }
        public virtual DbSet<Pais> Pais { get; set; }
        public virtual DbSet<Director> Director { get; set; }
        public virtual DbSet<Sede> Sede { get; set; }
        //public virtual DbSet<Responsable> Responsable { get; set; }


    }
}
