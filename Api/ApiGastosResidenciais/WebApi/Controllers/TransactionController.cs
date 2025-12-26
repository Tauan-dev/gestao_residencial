using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiGastosResidenciais.Application.DTOs.Transaction;
using ApiGastosResidenciais.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiGastosResidenciais.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(
            ITransactionService transactionService,
            ILogger<TransactionController> logger)
        {
            _transactionService = transactionService;
            _logger = logger;
        }

      
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TransactionDto>>> GetAll()
        {
            _logger.LogInformation("Iniciando GET api/transaction");

            try
            {
                var transactions = await _transactionService.GetAllAsync();

                if (!transactions.Any())
                {
                    _logger.LogWarning("Nenhuma transação encontrada");
                    return NotFound("Nenhuma transação encontrada");
                }

                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter transações");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter transações");
            }
        }

        
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TransactionDto>> GetById(int id)
        {
            _logger.LogInformation("Iniciando GET api/transaction/{Id}", id);

            try
            {
                var transaction = await _transactionService.GetByIdAsync(id);
                return Ok(transaction);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Transação não encontrada: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter transação {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter transação");
            }
        }

     
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Create([FromBody] CreateTransactionDto dto)
        {
            _logger.LogInformation("Iniciando POST api/transaction");

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido ao criar transação");
                return BadRequest(ModelState);
            }

            try
            {
                await _transactionService.CreateAsync(dto);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Erro de entidade relacionada ao criar transação");
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Regra de negócio violada ao criar transação");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar transação");
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar transação");
            }
        }

        
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Update(int id, [FromBody] UpdateTransactionDto dto)
        {
            _logger.LogInformation("Iniciando PUT api/transaction/{Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Modelo inválido ao atualizar transação {Id}", id);
                return BadRequest(ModelState);
            }

            try
            {
                await _transactionService.UpdateAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Transação ou entidade relacionada não encontrada ao atualizar: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Regra de negócio violada ao atualizar transação {Id}", id);
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar transação {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao atualizar transação");
            }
        }

        
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Delete(int id)
        {
            _logger.LogInformation("Iniciando DELETE api/transaction/{Id}", id);

            try
            {
                await _transactionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Transação não encontrada ao deletar: {Id}", id);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao deletar transação {Id}", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar transação");
            }
        }
    }
}
