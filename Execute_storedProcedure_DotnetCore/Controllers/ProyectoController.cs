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
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Director")]
    [Authorize(Roles = "Responsable")]
    public class ProyectoController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public ProyectoController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var proyectoList = await _dbContext.Proyecto.FromSqlRaw("EXEC sp_GetAllProyectos").ToListAsync();
            return Ok(proyectoList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var sqlStr = $"EXEC sp_GetProyecto @Id={Id}";
            var proyecto = await _dbContext.Proyecto.FromSqlRaw(sqlStr).SingleOrDefaultAsync();

            if (proyecto == null)
            {
                return NotFound("Proyecto no encontrado.");
            }

            return Ok(proyecto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Proyecto newProyecto)
        {
            if (newProyecto == null)
            {
                return BadRequest("Objeto de proyecto inválido");
            }

            try
            {
                // Ejecutar el stored procedure para insertar un nuevo proyecto
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarProyecto @Titulo, @Fecha_Inicio, @Fecha_Fin, @Presupuesto, @IdResponsable, @IdSede, @Status",
                    new SqlParameter("@Titulo", SqlDbType.NVarChar, 255) { Value = newProyecto.Titulo },
                    new SqlParameter("@Fecha_Inicio", SqlDbType.DateTime) { Value = newProyecto.Fecha_Inicio },
                    new SqlParameter("@Fecha_Fin", SqlDbType.DateTime) { Value = newProyecto.Fecha_Fin },
                    new SqlParameter("@Presupuesto", SqlDbType.Money) { Value = newProyecto.Presupuesto },
                    new SqlParameter("@IdResponsable", SqlDbType.Int) { Value = newProyecto.IdResponsable },
                    new SqlParameter("@IdSede", SqlDbType.Int) { Value = newProyecto.IdSede },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = newProyecto.Status });

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = newProyecto.IdProyecto }, newProyecto);
                }
                else
                {
                    return StatusCode(500, "Error al insertar el proyecto en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Proyecto updatedProyecto)
        {
            if (updatedProyecto == null || updatedProyecto.IdProyecto != Id)
            {
                return BadRequest("Objeto de proyecto inválido o Id no coincidente");
            }

            try
            {
                // Ejecutar el stored procedure para actualizar el proyecto
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdateProyecto @Id, @Titulo, @Fecha_Inicio, @Fecha_Fin, @Presupuesto, @IdResponsable, @IdSede, @Status",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@Titulo", SqlDbType.NVarChar, 255) { Value = updatedProyecto.Titulo },
                    new SqlParameter("@Fecha_Inicio", SqlDbType.DateTime) { Value = updatedProyecto.Fecha_Inicio },
                    new SqlParameter("@Fecha_Fin", SqlDbType.DateTime) { Value = updatedProyecto.Fecha_Fin },
                    new SqlParameter("@Presupuesto", SqlDbType.Money) { Value = updatedProyecto.Presupuesto },
                    new SqlParameter("@IdResponsable", SqlDbType.Int) { Value = updatedProyecto.IdResponsable },
                    new SqlParameter("@IdSede", SqlDbType.Int) { Value = updatedProyecto.IdSede },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = updatedProyecto.Status });

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedProyecto);
                }
                else
                {
                    return NotFound("Proyecto no encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
                // Ejecutar el stored procedure para eliminar el proyecto
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeleteProyecto @Id",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id });

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Proyecto no encontrado.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }
}
