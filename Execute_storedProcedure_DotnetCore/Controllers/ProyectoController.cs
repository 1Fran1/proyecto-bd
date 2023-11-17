using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.Data;
using Microsoft.AspNetCore.Authorization;

namespace Execute_storedProcedure_DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class ProyectoController : ControllerBase
    {
        private readonly MiApiContext _context;

        public ProyectoController(MiApiContext dbContext)
        {
            _context = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetProyecto(int id)
        {
            var proyecto = _context.Proyecto.FromSqlRaw("EXEC sp_GetProyecto @Id", new SqlParameter("@Id", id)).ToList();
            return Ok(proyecto);
        }

        [HttpGet]
        public IActionResult GetAllProyectos()
        {
            var proyectos = _context.Proyecto.FromSqlRaw("EXEC sp_GetAllProyectos").ToList();
            return Ok(proyectos);
        }

        [HttpPost("Insertar")]
        public IActionResult InsertarProyecto(Proyecto proyecto)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_IngresarProyecto @Titulo, @Fecha_Inicio, @Fecha_Fin, @Presupuesto, @IdResponsable, @IdSede, @Status",
                new SqlParameter("@Titulo", proyecto.Titulo),
                new SqlParameter("@Fecha_Inicio", proyecto.Fecha_Inicio),
                new SqlParameter("@Fecha_Fin", proyecto.Fecha_Fin),
                new SqlParameter("@Presupuesto", proyecto.Presupuesto),
                new SqlParameter("@IdResponsable", proyecto.IdResponsable),
                new SqlParameter("@IdSede", proyecto.IdSede),
                new SqlParameter("@Status", proyecto.Status));

            return Ok("Proyecto insertado correctamente");
        }

        [HttpPut("Actualizar/{id}")]
        public IActionResult ActualizarProyecto(int id, Proyecto proyecto)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_UpdateProyecto @Id, @Titulo, @Fecha_Inicio, @Fecha_Fin, @Presupuesto, @IdResponsable, @IdSede, @Status",
                new SqlParameter("@Id", id),
                new SqlParameter("@Titulo", proyecto.Titulo),
                new SqlParameter("@Fecha_Inicio", proyecto.Fecha_Inicio),
                new SqlParameter("@Fecha_Fin", proyecto.Fecha_Fin),
                new SqlParameter("@Presupuesto", proyecto.Presupuesto),
                new SqlParameter("@IdResponsable", proyecto.IdResponsable),
                new SqlParameter("@IdSede", proyecto.IdSede),
                new SqlParameter("@Status", proyecto.Status));

            return Ok("Proyecto actualizado correctamente");
        }

        [HttpDelete("Eliminar/{id}")]
        public IActionResult EliminarProyecto(int id)
        {
            _context.Database.ExecuteSqlRaw("EXEC sp_DeleteProyecto @Id", new SqlParameter("@Id", id));

            return Ok("Proyecto eliminado correctamente");
        }
    }
}
