using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.Data;

namespace Execute_storedProcedure_DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class PoblacionActuacionController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public PoblacionActuacionController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var poblacionActuacionList = await _dbContext.PoblacionActuacion.FromSqlRaw("sp_GetAllPoblacionActuacion").ToListAsync();
            return Ok(poblacionActuacionList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Sqlstr = "EXEC sp_GetPoblacionActuacion @Id=" + Id;
            var poblacionActuacionList = await _dbContext.PoblacionActuacion.FromSqlRaw(Sqlstr).ToListAsync();
            return Ok(poblacionActuacionList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PoblacionActuacion newPoblacionActuacion)
        {
            if (newPoblacionActuacion == null)
            {
                return BadRequest("Objeto de población actuación inválido");
            }

            try
            {
                var idPoblacionParam = new SqlParameter("@idPoblacion", SqlDbType.Int)
                {
                    Value = newPoblacionActuacion.idPoblacion
                };

                var idActuacionParam = new SqlParameter("@idActuacion", SqlDbType.Int)
                {
                    Value = newPoblacionActuacion.idActuacion
                };

                // Ejecutar el stored procedure para insertar una nueva población actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarPoblacionActuacion @idPoblacion, @idActuacion", idPoblacionParam, idActuacionParam);

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = result }, newPoblacionActuacion);
                }
                else
                {
                    return StatusCode(500, "Error al insertar la población actuación en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] PoblacionActuacion updatedPoblacionActuacion)
        {
            if (updatedPoblacionActuacion == null || updatedPoblacionActuacion.IdPoblacionActuacion != Id)
            {
                return BadRequest("Objeto de población actuación inválido o Id no coincidente");
            }

            try
            {
                var idPoblacionParam = new SqlParameter("@idPoblacion", SqlDbType.Int)
                {
                    Value = updatedPoblacionActuacion.idPoblacion
                };

                var idActuacionParam = new SqlParameter("@idActuacion", SqlDbType.Int)
                {
                    Value = updatedPoblacionActuacion.idActuacion
                };

                var idPoblacionActuacionParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = Id
                };

                // Ejecutar el stored procedure para actualizar la población actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdatePoblacionActuacion @Id, @idPoblacion, @idActuacion", idPoblacionActuacionParam, idPoblacionParam, idActuacionParam);

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedPoblacionActuacion);
                }
                else
                {
                    return NotFound("Población actuación no encontrada.");
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
                var idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = Id
                };

                // Ejecutar el stored procedure para eliminar la población actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeletePoblacionActuacion @Id", idParam);

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Población actuación no encontrada.");
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
