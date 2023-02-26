

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class NearPlaceController : ControllerBase
	{
		private readonly INearPlaceService _nearPlaceService;
		public NearPlaceController(INearPlaceService nearPlaceService)
		{
			_nearPlaceService = nearPlaceService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _nearPlaceService.GetAllAsync();
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
				var slider = await _nearPlaceService.GetByCondition(x => x.Title == title);
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
				var slider = await _nearPlaceService.GetByIdAsync(id);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}


		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateNearPlaceDto createNear)
		{
			try
			{
				await _nearPlaceService.Create(createNear);
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
		public async Task<IActionResult> Put(int id, [FromForm] UpdateNearPlaceDto updateNear)
		{
			try
			{
				await _nearPlaceService.UpdateAsync(id, updateNear);
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
				await _nearPlaceService.Delete(id);
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
