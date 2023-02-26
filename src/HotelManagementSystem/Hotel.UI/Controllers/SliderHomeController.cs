

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SliderHomeController : ControllerBase
	{
		private readonly ISliderHomeService _sliderHomeService;
		public SliderHomeController(ISliderHomeService sliderHomeService)
		{
			_sliderHomeService = sliderHomeService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _sliderHomeService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByTitle/{title}")]
		public async Task<IActionResult> GetByTitle(string title)
		{
			try
			{
				var slider = await _sliderHomeService.GetByCondition(x => x.Title == title);
				return Ok(slider);
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
				var slider = await _sliderHomeService.GetByIdAsync(id);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}


		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateSliderHomeDto sliderHomeDto)
		{
			try
			{
				await _sliderHomeService.Create(sliderHomeDto);
				return Ok("Created");
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm]UpdateSliderHomeDto updateSlider)
		{
			try
			{
				await _sliderHomeService.UpdateAsync(id, updateSlider);
				return Ok("Updated");
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectIdException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
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
				await _sliderHomeService.Delete(id);
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
