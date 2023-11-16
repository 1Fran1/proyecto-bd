using Execute_storedProcedure_DotnetCore.Data;
using Execute_storedProcedure_DotnetCore.Models;
using Execute_storedProcedure_DotnetCore.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Execute_storedProcedure_DotnetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultasController : ControllerBase
    {
        private readonly VM_Context _dbContext;

        public ConsultasController(VM_Context dbContext)
        {
            _dbContext = dbContext;
        }






        [HttpGet("consulta-proyecto-actuaciones-en-poblacion")]
        public async Task<IActionResult> ConsultaProyectoActuacionesEnPoblacion()
        {
            try
            {
                var result = await _dbContext.ProyectoActuacionPoblacion
                    .FromSqlRaw("Consulta_Proyecto_Actuaciones_En_Poblacion")
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }







                  [HttpGet("Consulta_Inversion_Proyecto_Poblacion")]
            public async Task<IActionResult> Consulta_Inversion_Proyecto_Poblacion()
            {
                try
                {
                    var result = await _dbContext.InversionProyectoPoblacion
                        .FromSqlRaw("Consulta_Inversion_Proyecto_Poblacion")
                        .ToListAsync();

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Error interno del servidor: {ex.Message}");
                }
            }






        [HttpGet("Consulta_ProyectoPoblacionHabitantes")]
        public async Task<IActionResult> Consulta_ProyectoPoblacionHabitantes()
        { 
            try
            {
                var result = await _dbContext.ProyectoPoblacionHabitantes
                    .FromSqlRaw("Consulta_cantidad_Habitates_Poblacion_Proyecto")
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }







        [HttpGet("Consulta_ContarSedesPorPais")]
        public async Task<IActionResult> Consulta_ContarSedesPorPais()
        {
            try
            {
                var result = await _dbContext.ContarSedesPorPaisModel
                    .FromSqlRaw("Consulta_ContarSedesPorPais")
                    .ToListAsync();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



















        [HttpGet("Consulta_ContarSedesPorPaisPorNombrePais/{Nombre}")]
        public async Task<IActionResult> Consulta_ContarSedesPorPaisPorNombrePais(string Nombre)
        {
            try
            {
                var Sqlstr = "EXEC Consulta_ContarSedesPorPaisPorNombrePais @Nombre";
                var paisList = await _dbContext.ContarSedesPorPaisModel
                    .FromSqlRaw(Sqlstr, new SqlParameter("@Nombre", Nombre))
                    .ToListAsync();

                if (paisList.Any())
                {
                    return Ok(paisList);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }



















        [HttpGet("Consulta_ObtenerInfoPoblacionPorIdPoblacion/{idPoblacion}")]
        public IActionResult Consulta_ObtenerInfoPoblacionPorIdPoblacion(int idPoblacion)
        {
            try
            {
                var result = _dbContext.Set<InfoPoblacionModel>()
                    .FromSqlRaw("Consulta_ObtenerInfoPoblacionPorIdPoblacion @idPoblacion",
                        new SqlParameter("@idPoblacion", idPoblacion))
                    .ToList();

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }























        [HttpGet("Consulta_ContarProyectosPorSedes/{idSede}")]
        public IActionResult Consulta_ContarProyectosPorSedes(int idSede)
        {
            try
            {
                var result = _dbContext.Set<ContarProyectosPorSedesModel>()
                    .FromSqlRaw("Consulta_ContarProyectosPorSedes @idSede",
                        new SqlParameter("@idSede", idSede))
                    .ToList();

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }












        [HttpGet("Consulta_ContarProyectosEnTodosLosPaises")]
        public IActionResult Consulta_ContarProyectosEnTodosLosPaises()
        {
            try
            {
                var result = _dbContext.Set<ContarProyectosEnTodosLosPaisesModel>()
                    .FromSqlRaw("Consulta_ContarProyectosEnTodosLosPaises")
                    .ToList();

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }










     




        [HttpGet("Consulta_ContarProyectosPorTodasSedes")]
        public IActionResult Consulta_ContarProyectosPorTodasSedes()
        {
            try
            {
                var result = _dbContext.Set<ContarProyectosPorSedesModel>()
                    .FromSqlRaw("Consulta_ContarProyectosPorTodasSedes")
                    .ToList();

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }















        [HttpGet("Consulta_BuscarProyectoEnSede/{tituloProyecto}")]
        public IActionResult Consulta_BuscarProyectoEnSede(string tituloProyecto)
        {
            try
            {
                var result = _dbContext.Set<BuscarProyectoEnSedeModel>()
                    .FromSqlRaw("Consulta_BuscarProyectoEnSede @tituloProyecto", new SqlParameter("@tituloProyecto", tituloProyecto))
                    .ToList();

                if (result.Any())
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }





    }
}
