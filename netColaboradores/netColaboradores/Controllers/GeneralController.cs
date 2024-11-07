using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using netColaboradores.Models;
using Microsoft.EntityFrameworkCore;

namespace netColaboradores.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {

        //4
        private readonly NetExamenContext dbContext;
        public GeneralController(NetExamenContext _dbContext)
        {
            dbContext = _dbContext;
        }

        //5
        [HttpGet]
        [Route("lista")]
        public async Task<IActionResult> Get()
        {
            var listColaborador = await dbContext.Colaboradors.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, listColaborador);
        }

        //6
        [HttpGet]
        [Route("obtener/{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var colaborador = await dbContext.Colaboradors.FirstOrDefaultAsync(e => e.IdColaborador == id);
            return StatusCode(StatusCodes.Status200OK, colaborador);
        }

        //Filtros Get
        [HttpGet]
        [Route("filtrar")]
        public async Task<IActionResult> GetFiltered([FromQuery] DateTime? fechaInicio, [FromQuery] DateTime? fechaFin, [FromQuery] bool? esProfesor)
        {
            var query = dbContext.Colaboradors.AsQueryable();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                query = query.Where(c => EF.Property<DateTime>(c, "FechaCreacion") >= fechaInicio.Value.Date && EF.Property<DateTime>(c, "FechaCreacion") <= fechaFin.Value.Date);
            }

            if (esProfesor.HasValue)
            {
                query = query.Where(c => c.IsProfesor == esProfesor.Value);
            }

            var result = await query.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        [HttpPost]
        [Route("crear")]
        public async Task<IActionResult> CreateColaborador([FromBody] ColaboradorRequest request)
        {
            if (request == null)
                return BadRequest("La información del colaborador no puede ser nula.");

            // Crear el registro del colaborador en la tabla "colaborador"
            var nuevoColaborador = new Colaborador
            {
                Nombre = request.Nombre,
                Edad = request.Edad,
                Birthday = DateOnly.FromDateTime(request.Birthday),
                IsProfesor = request.IsProfesor,
                FechaCreacion = DateOnly.FromDateTime(DateTime.Now)

            };

            dbContext.Colaboradors.Add(nuevoColaborador);
            await dbContext.SaveChangesAsync(); // Guardar para obtener el ID generado por la base de datos

            // Verificar si el colaborador es profesor o administrativo y agregar el registro en la tabla correspondiente
            if ((bool)nuevoColaborador.IsProfesor)
            {
                var nuevoProfesor = new Profesor
                {
                    Fkcolaborador = nuevoColaborador.IdColaborador,
                    Correo = request.Correo,
                    Departamento = request.Departamento
                };
                dbContext.Profesors.Add(nuevoProfesor);
            }
            else
            {
                var nuevoAdministrativo = new Administrativo
                {
                    Fkcolaborador = nuevoColaborador.IdColaborador,
                    Correo = request.Correo,
                    Puesto = request.Puesto,
                    Nomina = request.Nomina
                };
                dbContext.Administrativos.Add(nuevoAdministrativo);
            }

            await dbContext.SaveChangesAsync(); // Guardar los cambios en la tabla específica

            return StatusCode(StatusCodes.Status201Created, "Colaborador creado exitosamente.");
        }


    }
}
