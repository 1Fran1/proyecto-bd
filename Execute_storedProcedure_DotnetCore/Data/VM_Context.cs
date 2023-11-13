using Execute_storedProcedure_DotnetCore.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Execute_storedProcedure_DotnetCore.Data
{
    public class VM_Context : DbContext
    {



        public VM_Context(DbContextOptions<VM_Context> options) : base(options)
        {
        }

         public virtual DbSet<ConsultasPublicas> ConsultasPublicas { get; set; }

        public virtual DbSet<ProyectoActuacionPoblacion> ProyectoActuacionPoblacion { get; set; }

        public virtual DbSet<InversionProyectoPoblacion> InversionProyectoPoblacion { get; set; }

        public virtual DbSet<ProyectoPoblacionHabitantes> ProyectoPoblacionHabitantes { get; set; }

        public virtual DbSet<ContarSedesPorPaisModel> ContarSedesPorPaisModel { get; set; }

        public virtual DbSet<InfoPoblacionModel> InfoPoblacionModel { get; set; }

        public virtual DbSet<ContarProyectosPorSedesModel> ContarProyectosPorSedesModel { get; set; }

        public virtual DbSet<ContarProyectosEnTodosLosPaisesModel> ContarProyectosEnTodosLosPaisesModel { get; set; }

        public virtual DbSet<BuscarProyectoEnSedeModel> BuscarProyectoEnSedeModel { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsultasPublicas>().HasNoKey();

            modelBuilder.Entity<ProyectoActuacionPoblacion>().HasNoKey();

            modelBuilder.Entity<InversionProyectoPoblacion>().HasNoKey();

            modelBuilder.Entity<ProyectoPoblacionHabitantes>().HasNoKey();

            modelBuilder.Entity<ContarSedesPorPaisModel>().HasNoKey();

            modelBuilder.Entity<InfoPoblacionModel>().HasNoKey();

            modelBuilder.Entity<ContarProyectosPorSedesModel>().HasNoKey();

            modelBuilder.Entity<ContarProyectosEnTodosLosPaisesModel>().HasNoKey();

            modelBuilder.Entity<BuscarProyectoEnSedeModel>().HasNoKey();





            // Otros ajustes del modelo...
        }

    }
}
