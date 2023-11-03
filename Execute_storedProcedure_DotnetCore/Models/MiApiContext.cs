using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace Execute_storedProcedure_DotnetCore.Models
{


    //public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    //{
    //    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    //    {

    //    }
    //}






    //public class MiApiContext : DbContext
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

        //public virtual DbSet<>



        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{


        //------------relaciones de 1 a 1----------------------------//
        // Relación Uno a Uno entre sede y director
        //modelBuilder.Entity<Sede>()
        //        .HasOne(d => d.Director)
        //        .WithOne()
        //        .HasForeignKey<Director>(d => d.IdDirector);



        //    // Relación Uno a Uno entre responsable y proyecto
        //    modelBuilder.Entity<Proyecto>()
        //        .HasOne(a => a.Responsable)
        //        .WithOne()
        //        .HasForeignKey<Proyecto>(d => d.IdResponsable);

        //    // Relación Uno a Uno entre sede y pais
        //    modelBuilder.Entity<Sede>()
        //       .HasOne(a => a.Pais)
        //       .WithOne()
        //       .HasForeignKey<Sede>(d => d.IdPais);


        //    //Relación Uno a Uno entre poblacion y pais
        //    modelBuilder.Entity<Poblacion>()
        //       .HasOne(a => a.Pais)
        //       .WithOne()
        //       .HasForeignKey<Poblacion>(d => d.IdPais);


        //    //------------fin de relaciones de 1 a 1----------------------------//



        //    //------------relaciones de 1 a Muchos----------------------------//

        //    //modelBuilder.Entity<Pais>()
        //    //    .HasMany(d => d.Sedes)
        //    //    .WithOne(e => e.Pais)
        //    //    .HasForeignKey(e => e.IdPais);

        //    // Relación Uno a Muchos entre sede y proyecto
        //    modelBuilder.Entity<Sede>()
        //        .HasMany(d => d.Proyectos)
        //        .WithOne(e => e.Sede)
        //        .HasForeignKey(e => e.IdProyecto);

        //    // Relación Uno a Muchos entre Actuacion y Poblacion
        //    modelBuilder.Entity<Actuacion>()
        //       .HasMany(d => d.Poblaciones)
        //       .WithOne(e => e.Actuacion)
        //       .HasForeignKey(e => e.IdPoblacion);

        //    //modelBuilder.Entity<Pais>()
        //    //.HasMany(d => d.Poblaciones)
        //    //.WithOne(e => e.Pais)
        //    //.HasForeignKey(e => e.IdPoblacion);




        //    // Relación Muchos a Muchos entre Estudiante y Curso
        //    //modelBuilder.Entity<CursoEstudiante>()
        //    //    .HasKey(ce => new { ce.EstudianteId, ce.CursoId });

        //    //modelBuilder.Entity<CursoEstudiante>()
        //    //    .HasOne(ce => ce.Estudiante)
        //    //    .WithMany(e => e.CursosEstudiantes)
        //    //    .HasForeignKey(ce => ce.EstudianteId);

        //    //modelBuilder.Entity<CursoEstudiante>()
        //    //    .HasOne(ce => ce.Curso)
        //    //    .WithMany(c => c.CursosEstudiantes)
        //    //    .HasForeignKey(ce => ce.CursoId);
        //}




    }
}