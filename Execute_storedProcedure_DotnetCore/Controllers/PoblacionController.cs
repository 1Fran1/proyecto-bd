using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
using Execute_storedProcedure_DotnetCore.Models;

namespace Execute_storedProcedure_DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PoblacionController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public PoblacionController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var poblacionList = await _dbContext.Poblacion.FromSqlRaw("EXEC sp_GetAllPoblaciones").ToListAsync();
            return Ok(poblacionList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var sqlStr = $"EXEC sp_GetPoblacion @Id={Id}";
            var poblacion = await _dbContext.Poblacion.FromSqlRaw(sqlStr).SingleOrDefaultAsync();

            if (poblacion == null)
            {
                return NotFound("Población no encontrada.");
            }

            return Ok(poblacion);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Poblacion newPoblacion)
        {
            if (newPoblacion == null)
            {
                return BadRequest("Objeto de población inválido");
            }

            try
            {
                // Ejecutar el stored procedure para insertar una nueva población
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarPoblacion @Nombre, @IdPais, @NumHabitantes, @Descripcion, @IdActuacion, @Status",
                    new SqlParameter("@Nombre", SqlDbType.NVarChar, 255) { Value = newPoblacion.Nombre },
                    new SqlParameter("@IdPais", SqlDbType.Int) { Value = newPoblacion.IdPais },
                    new SqlParameter("@NumHabitantes", SqlDbType.Int) { Value = newPoblacion.NumHabitantes },
                    new SqlParameter("@Descripcion", SqlDbType.NVarChar, 255) { Value = newPoblacion.Descripcion },
                    new SqlParameter("@IdActuacion", SqlDbType.Int) { Value = newPoblacion.IdActuacion },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = newPoblacion.Status });

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = newPoblacion.IdPoblacion }, newPoblacion);
                }
                else
                {
                    return StatusCode(500, "Error al insertar la población en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Poblacion updatedPoblacion)
        {
            if (updatedPoblacion == null || updatedPoblacion.IdPoblacion != Id)
            {
                return BadRequest("Objeto de población inválido o Id no coincidente");
            }

            try
            {
                // Ejecutar el stored procedure para actualizar la población
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdatePoblacion @Id, @Nombre, @IdPais, @NumHabitantes, @Descripcion, @IdActuacion, @Status",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@Nombre", SqlDbType.NVarChar, 255) { Value = updatedPoblacion.Nombre },
                    new SqlParameter("@IdPais", SqlDbType.Int) { Value = updatedPoblacion.IdPais },
                    new SqlParameter("@NumHabitantes", SqlDbType.Int) { Value = updatedPoblacion.NumHabitantes },
                    new SqlParameter("@Descripcion", SqlDbType.NVarChar, 255) { Value = updatedPoblacion.Descripcion },
                    new SqlParameter("@IdActuacion", SqlDbType.Int) { Value = updatedPoblacion.IdActuacion },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = updatedPoblacion.Status });

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedPoblacion);
                }
                else
                {
                    return NotFound("Población no encontrada.");
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
                // Ejecutar el stored procedure para eliminar la población
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeletePoblacion @Id",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id });

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Población no encontrada.");
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
