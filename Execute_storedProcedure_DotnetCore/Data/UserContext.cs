using Execute_storedProcedure_DotnetCore.Models;
using Microsoft.EntityFrameworkCore;

namespace Execute_storedProcedure_DotnetCore.Data
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {
        }
        //public virtual DbSet<Pais> Pais { get; set; }

    }
}
