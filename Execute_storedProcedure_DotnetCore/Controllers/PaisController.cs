using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;
using System.Threading.Tasks;
using Execute_storedProcedure_DotnetCore.Models;
using Microsoft.AspNetCore.Authorization;
using Execute_storedProcedure_DotnetCore.Data;

namespace Execute_storedProcedure_DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    public class PaisController : ControllerBase
    {
        //private readonly UserContext _dbcontext;
        //public PaisController(UserContext dbConext)
        //{
        //    _dbcontext = dbConext;
        //}


        private readonly MiApiContext _dbContext;

        public PaisController(MiApiContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var paisList = await _dbContext.Pais.FromSqlRaw("sp_GetAllPaises").ToListAsync();
            return Ok(paisList);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById(int Id)
        {
            var Sqlstr = "EXEC sp_GetPais @Id=" + Id;
            var paisList = await _dbContext.Pais.FromSqlRaw(Sqlstr).ToListAsync();
            return Ok(paisList);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pais newPais)
        {
            if (newPais == null)
            {
                return BadRequest("Objeto de país inválido");
            }

            try
            {
                var nombreParam = new SqlParameter("@Nombre", SqlDbType.NVarChar, 255)
                {
                    Value = newPais.Nombre
                };

                var statusParam = new SqlParameter("@Status", SqlDbType.Int)
                {
                    Value = newPais.Status
                };

                // Ejecutar el stored procedure para insertar un nuevo país
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_IngresarPais @Nombre, @Status", nombreParam, statusParam);

                // Devolver una respuesta 201 Created si la inserción fue exitosa
                if (result > 0)
                {
                    return CreatedAtAction(nameof(GetById), new { Id = result }, newPais);
                }
                else
                {
                    return StatusCode(500, "Error al insertar el país en la base de datos.");
                }
            }
            catch (Exception ex)
            {
                // Manejar cualquier error que ocurra durante la operación de base de datos
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpPut("{Id}")]
        public async Task<IActionResult> Put(int Id, [FromBody] Pais updatedPais)
        {
            if (updatedPais == null || updatedPais.IdPais != Id)
            {
                return BadRequest("Objeto de país inválido o Id no coincidente");
            }

            try
            {
                var nombreParam = new SqlParameter("@Nombre", SqlDbType.NVarChar, 255)
                {
                    Value = updatedPais.Nombre
                };

                var statusParam = new SqlParameter("@Status", SqlDbType.Int)
                {
                    Value = updatedPais.Status
                };

                var idParam = new SqlParameter("@Id", SqlDbType.Int)
                {
                    Value = Id
                };

                // Ejecutar el stored procedure para actualizar el país
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_UpdatePais @Id, @Nombre, @Status", idParam, nombreParam, statusParam);

                // Devolver una respuesta 200 OK si la actualización fue exitosa
                if (result > 0)
                {
                    return Ok(updatedPais);
                }
                else
                {
                    return NotFound("País no encontrado.");
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

                // Ejecutar el stored procedure para eliminar el país
                var result = await _dbContext.Database.ExecuteSqlRawAsync("sp_DeletePais @Id", idParam);

                // Devolver una respuesta 204 No Content si la eliminación fue exitosa
                if (result > 0)
                {
                    return NoContent();
                }
                else
                {
                    return NotFound("País no encontrado.");
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
