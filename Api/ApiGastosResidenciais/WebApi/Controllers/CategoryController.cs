using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Category;
using ApiGastosResidenciais.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGastosResidenciais.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
        }

     
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<CategoryDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando GET api/category");

            try
            {
                var categories = await _categoryService.GetAllAsync();

                if (!categories.Any())
                {
                    _logger.LogWarning("Nenhuma categoria encontrada");
                    return NotFound("Nenhuma categoria encontrada");
                }

                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter categorias");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter categorias");
            }
        }

    
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            _logger.LogInformation("Iniciando GET api/category/{Id}", id);

            try
            {
                var category = await _categoryService.GetByIdAsync(id);
                return Ok(category);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Categoria não encontrada: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter categoria {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter categoria");
            }
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            _logger.LogInformation("Iniciando POST api/category");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido ao criar categoria");
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch(ApplicationException ex)
            {
                _logger.LogWarning(ex, "Erro ao criar categoria");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar categoria");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar categoria");
            }
        }

    
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateCategoryDto dto)
        {
            _logger.LogInformation("Iniciando PUT api/category/{Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido ao atualizar categoria {Id}", id);
                return BadRequest(ModelState);
            }

            try
            {
                await _categoryService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Categoria não encontrada ao atualizar: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar categoria {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar categoria");
            }
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando DELETE api/category/{Id}", id);

            try
            {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Categoria não encontrada ao deletar: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar categoria {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar categoria");
            }
        }

        [HttpGet("totals")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        public async Task<ActionResult<object>> GetTotals()
        {
            _logger.LogInformation("Iniciando GET api/category/totals");

            try
            {
                var (categories, total) = await _categoryService.GetTotalsByCategoryAsync();

                var response = new
                {
                    categories,
                    total
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter totais por categoria");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter totais por categoria");
            }
        }


        [HttpGet("spent")]
        [ProducesResponseType(typeof(SpentResult[]), StatusCodes.Status200OK)]
        public async Task<ActionResult<SpentResult[]>> GetSpent()
        {
            _logger.LogInformation("Iniciando GET api/category/spent");

            try
            {
                var spent = await _categoryService.GetSpentAsync();
                return Ok(spent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter menor/maior gasto por categoria");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter menor/maior gasto por categoria");
            }
        }
    }
}
