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
    //[Authorize(Roles = "Director")]
    //[Authorize(Roles = "Responsable")]
    public class Poblacion_ActuacionController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public Poblacion_ActuacionController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Poblacion_ActuacionList = await _dbContext.PoblacionActuacion.FromSqlRaw("sp_GetAllPoblacion_Actuacion").ToListAsync();
            return Ok(Poblacion_ActuacionList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Sqlstr = "EXEC sp_GetPoblacion_Actuacion @Id=" + Id;
            var Poblacion_ActuacionList = await _dbContext.PoblacionActuacion.FromSqlRaw(Sqlstr).ToListAsync();
            return Ok(Poblacion_ActuacionList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PoblacionActuacion newPoblacion_Actuacion)
        {
            if (newPoblacion_Actuacion == null)
            {
                return BadRequest("Objeto de población actuación inválido");
            }

            try
            {
                var idPoblacionParam = new SqlParameter("@idPoblacion", SqlDbType.Int)
                {
                    Value = newPoblacion_Actuacion.idPoblacion
                };

                var idActuacionParam = new SqlParameter("@idActuacion", SqlDbType.Int)
                {
                    Value = newPoblacion_Actuacion.idActuacion
                };

                // Ejecutar el stored procedure para insertar una nueva población actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarPoblacion_Actuacion @idPoblacion, @idActuacion", idPoblacionParam, idActuacionParam);

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = result }, newPoblacion_Actuacion);
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
        public async Task<IActionResult> Put(int Id, [FromBody] PoblacionActuacion updatedPoblacion_Actuacion)
        {
            if (updatedPoblacion_Actuacion == null || updatedPoblacion_Actuacion.IdPoblacion_Actuacion != Id)
            {
                return BadRequest("Objeto de población actuación inválido o Id no coincidente");
            }

            try
            {
                var idPoblacionParam = new SqlParameter("@idPoblacion", SqlDbType.Int)
                {
                    Value = updatedPoblacion_Actuacion.idPoblacion
                };

                var idActuacionParam = new SqlParameter("@idActuacion", SqlDbType.Int)
                {
                    Value = updatedPoblacion_Actuacion.idActuacion
                };

                var idPoblacion_ActuacionParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = Id
                };

                // Ejecutar el stored procedure para actualizar la población actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdatePoblacion_Actuacion @Id, @idPoblacion, @idActuacion", idPoblacion_ActuacionParam, idPoblacionParam, idActuacionParam);

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedPoblacion_Actuacion);
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
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeletePoblacion_Actuacion @Id", idParam);

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
