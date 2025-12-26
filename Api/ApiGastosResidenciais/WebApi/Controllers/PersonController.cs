using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Person;
using ApiGastosResidenciais.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGastosResidenciais.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PersonDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PersonDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando GET api/person");

            try
            {
                var people = await _personService.GetAllAsync();

                if (!people.Any())
                {
                    _logger.LogWarning("Nenhuma pessoa encontrada");
                    return NotFound("Nenhuma pessoa encontrada");
                }

                return Ok(people);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pessoas");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter pessoas");
            }
        }


        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(PersonDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PersonDto>> GetById(int id)
        {
            _logger.LogInformation("Iniciando GET api/person/{Id}", id);

            try
            {
                var person = await _personService.GetByIdAsync(id);
                return Ok(person);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Pessoa não encontrada: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter pessoa {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter pessoa");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreatePersonDto dto)
        {
            _logger.LogInformation("Iniciando POST api/person");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido ao criar pessoa");
                return BadRequest(ModelState);
            }

            try
            {
                await _personService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar pessoa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar pessoa");
            }
        }


        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdatePersonDto dto)
        {
            _logger.LogInformation("Iniciando PUT api/person/{Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido ao atualizar pessoa {Id}", id);
                return BadRequest(ModelState);
            }

            try
            {
                await _personService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Pessoa não encontrada ao atualizar: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar pessoa {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar pessoa");
            }
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando DELETE api/person/{Id}", id);

            try
            {
                await _personService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Pessoa não encontrada ao deletar: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar pessoa {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar pessoa");
            }
        }


        [HttpGet("totals")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> GetTotals()
        {
            _logger.LogInformation("Iniciando GET api/person/totals");

            try
            {
                var (items, total) = await _personService.GetTotalsByPersonAsync();

                var response = new
                {
                    people = items,
                    total
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter totais por pessoa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter totais por pessoa");
            }
        }


        [HttpGet("spent")]
        [ProducesResponseType(typeof(SpentResult[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<SpentResult[]>> GetSpent()
        {
            _logger.LogInformation("Iniciando GET api/person/spent");

            try
            {
                var spent = await _personService.GetSpentAsync();
                return Ok(spent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter menor/maior gasto por pessoa");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter menor/maior gasto por pessoa");
            }
        }
    }
}
