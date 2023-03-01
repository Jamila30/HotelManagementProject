using Hotel.Business.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;

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

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
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

		[Authorize(Roles = "Admin")]
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
