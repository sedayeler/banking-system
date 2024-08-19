using Microsoft.AspNetCore.Mvc;
using Models.DTOs.Transaction;
using Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionsController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [HttpPost("AddTransaction")]
        public IActionResult Add(CreateTransactionDto dto)
        {
            var result = _transactionService.Add(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateTransaction")]
        public IActionResult Update(UpdateTransactionDto dto)
        {
            var result = _transactionService.Update(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteTransaction")]
        public IActionResult Delete(int id)
        {
            var result = _transactionService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Get")]
        public IActionResult GetById(int id)
        {
            var result = _transactionService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _transactionService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
