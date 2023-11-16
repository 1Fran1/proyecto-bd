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
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Director")]
    [Authorize(Roles = "Responsable")]
    [ApiController]
    public class ActuacionController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public ActuacionController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var actuacionList = await _dbContext.Actuacion.FromSqlRaw("EXEC sp_GetAllActuaciones").ToListAsync();
            return Ok(actuacionList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var sqlStr = $"EXEC sp_GetActuacion @Id={Id}";
            var actuacion = await _dbContext.Actuacion.FromSqlRaw(sqlStr).SingleOrDefaultAsync();

            if (actuacion == null)
            {
                return NotFound("Actuación no encontrada.");
            }

            return Ok(actuacion);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Actuacion newActuacion)
        {
            if (newActuacion == null)
            {
                return BadRequest("Objeto de actuación inválido");
            }

            try
            {
                // Ejecutar el stored procedure para insertar una nueva actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarActuacion @Presupuesto, @Nombre, @Descripcion, @IdProyecto, @Status",
                    new SqlParameter("@Presupuesto", SqlDbType.Money) { Value = newActuacion.Presupuesto },
                    new SqlParameter("@Nombre", SqlDbType.NVarChar, 255) { Value = newActuacion.Nombre },
                    new SqlParameter("@Descripcion", SqlDbType.NVarChar, 255) { Value = newActuacion.Descripcion },
                    new SqlParameter("@IdProyecto", SqlDbType.Int) { Value = newActuacion.IdProyecto },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = newActuacion.Status });

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = newActuacion.IdActuacion }, newActuacion);
                }
                else
                {
                    return StatusCode(500, "Error al insertar la actuación en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Actuacion updatedActuacion)
        {
            if (updatedActuacion == null || updatedActuacion.IdActuacion != Id)
            {
                return BadRequest("Objeto de actuación inválido o Id no coincidente");
            }

            try
            {
                // Ejecutar el stored procedure para actualizar la actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdateActuacion @Id, @Presupuesto, @Nombre, @Descripcion, @IdProyecto, @Status",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@Presupuesto", SqlDbType.Money) { Value = updatedActuacion.Presupuesto },
                    new SqlParameter("@Nombre", SqlDbType.NVarChar, 255) { Value = updatedActuacion.Nombre },
                    new SqlParameter("@Descripcion", SqlDbType.NVarChar, 255) { Value = updatedActuacion.Descripcion },
                    new SqlParameter("@IdProyecto", SqlDbType.Int) { Value = updatedActuacion.IdProyecto },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = updatedActuacion.Status });

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedActuacion);
                }
                else
                {
                    return NotFound("Actuación no encontrada.");
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
                // Ejecutar el stored procedure para eliminar la actuación
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeleteActuacion @Id",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id });

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Actuación no encontrada.");
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
