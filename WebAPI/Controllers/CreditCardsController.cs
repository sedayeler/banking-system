using Microsoft.AspNetCore.Mvc;
using Models.DTOs.CreditCard;
using Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardsController : ControllerBase
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardsController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpPost("AddCreditCard")]
        public IActionResult Add(CreateCreditCardDto dto)
        {
            var result = _creditCardService.Add(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("UpdateCreditCard")]
        public IActionResult Update(UpdateCreditCardDto dto)
        {
            var result = _creditCardService.Update(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("DeleteCreditCard")]
        public IActionResult Delete(int id)
        {
            var result = _creditCardService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("Get")]
        public IActionResult Get(int id)
        {
            var result = _creditCardService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _creditCardService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
