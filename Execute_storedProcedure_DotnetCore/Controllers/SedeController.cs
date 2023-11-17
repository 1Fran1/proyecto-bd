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
    //[Authorize(Roles = "Admin")]
    public class SedeController : ControllerBase
    {
        private readonly MiApiContext _dbContext;

        public SedeController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }




        [HttpGet]
        public async Task<IActionResult> GetAllSedes()
        {
            try
            {
                var sedes = await _dbContext.Sede.ToListAsync();
                return Ok(sedes);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSedeById(int id)
        {
            try
            {
                var sede = await _dbContext.Sede.FirstOrDefaultAsync(s => s.IdSede == id);
                if (sede == null)
                {
                    return NotFound();
                }

                return Ok(sede);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> IngresarSede([FromBody] Sede nuevaSede)
        {
            if (nuevaSede == null)
            {
                return BadRequest("Objeto de sede inválido");
            }

            try
            {
                var result = await _dbContext.Database.ExecuteSqlRawAsync(
                    "EXEC sp_IngresarSede @IdPais, @IdDirector, @Ciudad, @FondoPresupuestario, @Direccion, @Telefono, @Status",
                    new SqlParameter("@IdPais", nuevaSede.IdPais),
                    new SqlParameter("@IdDirector", nuevaSede.IdDirector),
                    new SqlParameter("@Ciudad", nuevaSede.Ciudad),
                    new SqlParameter("@FondoPresupuestario", nuevaSede.Fondo_Presupuestario),
                    new SqlParameter("@Direccion", nuevaSede.Direccion),
                    new SqlParameter("@Telefono", nuevaSede.Telefono),
                    new SqlParameter("@Status", nuevaSede.Status)
                );

                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetSedeById), new { Id = nuevaSede.IdSede }, nuevaSede);
                }
                else
                {
                    return StatusCode(500, "Error al insertar la sede en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSede(int id, [FromBody] Sede sedeActualizada)
        {
            if (sedeActualizada == null || sedeActualizada.IdSede != id)
            {
                return BadRequest("Datos inválidos para la actualización.");
            }

            try
            {
                _dbContext.Sede.Update(sedeActualizada);
                await _dbContext.SaveChangesAsync();

                return Ok(sedeActualizada);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSede(int id)
        {
            try
            {
                var sede = await _dbContext.Sede.FirstOrDefaultAsync(s => s.IdSede == id);
                if (sede == null)
                {
                    return NotFound();
                }

                _dbContext.Sede.Remove(sede);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
    }

}