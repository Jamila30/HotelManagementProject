using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SentQuestionController : ControllerBase
	{
		private readonly ISentQuestionService _questionService;
		public SentQuestionController(ISentQuestionService questionService)
		{
			_questionService = questionService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _questionService.GetAllAsync();
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
				var question = await _questionService.GetByIdAsync(id);
				return Ok(question);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpGet("searchForEmail/{email}")]
		public async Task<IActionResult> GetByEmail(string email)
		{
			try
			{
				var question = await _questionService.GetByCondition(x=>x.Email== email);
				return Ok(question);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateSentQuestionDto createSentQuestion)
		{
			try
			{
				await _questionService.CreateQuestion(createSentQuestion);
				return Ok("Created");
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("AnswerQuestion")]
		public async Task<IActionResult> AnswerQuestion([FromForm] AnswerDto answerDto)
		{
			try
			{
				await _questionService.AnswerQuestion(answerDto);
				return Ok("Answered");
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
				await _questionService.Delete(id);
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
