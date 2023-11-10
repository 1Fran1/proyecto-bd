using Execute_storedProcedure_DotnetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Execute_storedProcedure_DotnetCore.Data
{
    public class DirectorContext : DbContext
    {
        public DirectorContext(DbContextOptions<DirectorContext> options) : base(options)
        {
        }
        public virtual DbSet<Responsable> Responsable { get; set; }
        public virtual DbSet<Proyecto> Proyecto { get; set; }

    }
}
