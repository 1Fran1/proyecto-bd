using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
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
    //[Authorize(Roles = "Director")]
    public class ResponsableController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public ResponsableController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var responsableList = await _dbContext.Responsable.FromSqlRaw("sp_GetAllResponsables").ToListAsync();
            return Ok(responsableList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Sqlstr = "EXEC sp_GetResponsable @id=" + Id;
            var responsableList = await _dbContext.Responsable.FromSqlRaw(Sqlstr).ToListAsync();
            return Ok(responsableList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Responsable newResponsable)
        {
            if (newResponsable == null)
            {
                return BadRequest("Objeto de responsable inválido");
            }

            try
            {
                // Ejecutar el stored procedure para insertar un nuevo responsable
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarResponsable @UserId, @Status",
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 255) { Value = newResponsable.UserId },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = newResponsable.Status });

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = newResponsable.IdResponsable }, newResponsable);
                }
                else
                {
                    return StatusCode(500, "Error al insertar el responsable en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Responsable updatedResponsable)
        {
            if (updatedResponsable == null || updatedResponsable.IdResponsable != Id)
            {
                return BadRequest("Objeto de responsable inválido o Id no coincidente");
            }

            try
            {
                // Ejecutar el stored procedure para actualizar el responsable
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdateResponsable @Id, @UserId, @Status",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 255) { Value = updatedResponsable.UserId },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = updatedResponsable.Status });

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    // Obtén el responsable actualizado desde la base de datos utilizando el Id
                    var updatedResponsableFromDb = await _dbContext.Responsable.FindAsync(Id);
                    return Ok(updatedResponsableFromDb);
                }
                else
                {
                    return NotFound("Responsable no encontrado.");
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
                // Ejecutar el stored procedure para eliminar el responsable
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeleteResponsable @Id",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id });

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Responsable no encontrado.");
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
