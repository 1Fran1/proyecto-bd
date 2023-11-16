using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.Data;
using Microsoft.AspNetCore.Authorization;

namespace Execute_storedProcedure_DotnetCore.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class SedeController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public SedeController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var sedeList = await _dbContext.Sede.FromSqlRaw("EXEC sp_GetAllSedes").ToListAsync();
            return Ok(sedeList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var sqlStr = $"EXEC sp_GetSede @Id={Id}";
            var sede = await _dbContext.Sede.FromSqlRaw(sqlStr).SingleOrDefaultAsync();

            if (sede == null)
            {
                return NotFound("Sede no encontrada.");
            }

            return Ok(sede);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Sede newSede)
        {
            if (newSede == null)
            {
                return BadRequest("Objeto de sede inválido");
            }

            try
            {
                // Ejecutar el stored procedure para insertar una nueva sede
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC sp_IngresarSede @IdPais, @IdDirector, @Ciudad, @Direccion, @Telefono, @Status",
                    new SqlParameter("@IdPais", SqlDbType.Int) { Value = newSede.IdPais },
                    new SqlParameter("@IdDirector", SqlDbType.Int) { Value = newSede.IdDirector },
                    new SqlParameter("@Ciudad", SqlDbType.NVarChar, 255) { Value = newSede.Ciudad },
                    new SqlParameter("@Direccion", SqlDbType.NVarChar, 255) { Value = newSede.Direccion },
                    new SqlParameter("@Telefono", SqlDbType.NVarChar, 255) { Value = newSede.Telefono },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = newSede.Status });

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = newSede.IdSede }, newSede);
                }
                else
                {
                    return StatusCode(500, "Error al insertar la sede en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Sede updatedSede)
        {
            if (updatedSede == null || updatedSede.IdSede != Id)
            {
                return BadRequest("Objeto de sede inválido o Id no coincidente");
            }

            try
            {
                // Ejecutar el stored procedure para actualizar la sede
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC sp_UpdateSede @Id, @IdPais, @IdDirector, @Ciudad, @Direccion, @Telefono, @Status",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id },
                    new SqlParameter("@IdPais", SqlDbType.Int) { Value = updatedSede.IdPais },
                    new SqlParameter("@IdDirector", SqlDbType.Int) { Value = updatedSede.IdDirector },
                    new SqlParameter("@Ciudad", SqlDbType.NVarChar, 255) { Value = updatedSede.Ciudad },
                    new SqlParameter("@Direccion", SqlDbType.NVarChar, 255) { Value = updatedSede.Direccion },
                    new SqlParameter("@Telefono", SqlDbType.NVarChar, 255) { Value = updatedSede.Telefono },
                    new SqlParameter("@Status", SqlDbType.Int) { Value = updatedSede.Status });

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedSede);
                }
                else
                {
                    return NotFound("Sede no encontrada.");
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
                // Ejecutar el stored procedure para eliminar la sede
                var result = await _dbContext.Database.ExecuteSqlRawAsync("EXEC sp_DeleteSede @Id",
                    new SqlParameter("@Id", SqlDbType.Int) { Value = Id });

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("Sede no encontrada.");
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
