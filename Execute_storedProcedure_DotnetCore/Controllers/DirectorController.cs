





using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.Data;
using Microsoft.AspNetCore.Authorization;

namespace Execute_storedProcedure_DotnetCore.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Admin")]
    [ApiController]
    public class DirectorController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public DirectorController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var directorList = await _dbContext.Director.FromSqlRaw("sp_GetAllDirectores").ToListAsync();
            return Ok(directorList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Sqlstr = $"EXEC sp_GetDirector @Id={Id}";
            var directorList = await _dbContext.Director.FromSqlRaw(Sqlstr).ToListAsync();
            return Ok(directorList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Director newDirector)
        {
            if (newDirector == null)
            {
                return BadRequest("Objeto de director inválido");
            }

            try
            {
                // Ejecutar el stored procedure para insertar un nuevo director
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarDirector @UserId, @Status",
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 255) { Value = newDirector.UserId },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = newDirector.Status });

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = newDirector.idDirector }, newDirector);
                }
                else
                {
                    return StatusCode(500, "Error al insertar el director en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Director updatedDirector)
        {
            if (updatedDirector == null || updatedDirector.idDirector != Id)
            {
                return BadRequest("Objeto de director inválido o Id no coincidente");
            }

            try
            {
                // Ejecutar el stored procedure para actualizar el director
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdateDirector @Id, @UserId, @Status",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@UserId", SqlDbType.NVarChar, 255) { Value = updatedDirector.UserId },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = updatedDirector.Status });

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedDirector);
                }
                else
                {
                    return NotFound("Director no encontrado.");
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
                // Ejecutar el stored procedure para eliminar el director
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeleteDirector @Id",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id });

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Director no encontrado.");
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
