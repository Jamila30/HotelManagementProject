using Hotel.Business.DTOs.FaqDTOs;
using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FaqController : ControllerBase
	{
		private readonly IFaqService _faqService;
		public FaqController(IFaqService faqService)
		{
			_faqService = faqService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _faqService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var question = await _faqService.GetByIdAsync(id);
				return Ok(question);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateFaqDto createFaq)
		{
			try
			{
				await _faqService.Create(createFaq);
				return Ok("Created");
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm] UpdateFaqDto updateFaq)
		{
			try
			{
				await _faqService.UpdateAsync(id, updateFaq);
				return Ok("Updated");
			}
			catch (IncorrectIdException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}

		}


		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(int id)
		{
			try
			{
				await _faqService.Delete(id);
				return Ok("Deleted");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);

			}
		}
	}
}
