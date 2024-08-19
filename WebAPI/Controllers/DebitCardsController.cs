using Microsoft.AspNetCore.Mvc;
using Models.DTOs.DebitCard;
using Services.Abstract;

namespace WebAPI.Controllers
{
    [Route("/api/[controller]")]
    [ApiController]
    public class DebitCardsController : ControllerBase
    {
        private readonly IDebitCardService _debitCardService;

        public DebitCardsController(IDebitCardService debitCardService)
        {
            _debitCardService = debitCardService;
        }

        [HttpPost("AddDebitCard")]
        public IActionResult Add(CreateDebitCardDto dto)
        {
            var result = _debitCardService.Add(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPut("UpdateDebitCard")]
        public IActionResult Update(UpdateDebitCardDto dto)
        {
            var result = _debitCardService.Update(dto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteDebitCard")]
        public IActionResult Delete(int id)
        {
            var result = _debitCardService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("Get")]
        public IActionResult Get(int id)
        {
            var result = _debitCardService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _debitCardService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
