using Hotel.Business.DTOs.AmentityDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AmentityController : ControllerBase
	{
		private readonly IAmentityService _amentityService;
		public AmentityController(IAmentityService amentityService)
		{
			_amentityService = amentityService;
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			try
			{
				var list = await _amentityService.GetAllAsync();
				return Ok(list);
			}
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet("{id}")]
		public async Task<ActionResult> GetById(int id)
		{
			try
			{
				var list = await _amentityService.GetByIdAsync(id);
				return Ok(list);
			}
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet("searchByTitle/{title}")]
		public async Task<ActionResult> GetByTitle(string title)
		{
			try
			{
				var list = await _amentityService.GetByCondition(x => x.Title == title);
				return Ok(list);
			}
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost]
		public async Task<ActionResult> Post([FromForm] CreateAmentityDto createAmentity)
		{
			try
			{
				await _amentityService.Create(createAmentity);
				return Ok("Created");
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromForm] UpdateAmentityDto updateAmentity)
		{
			try
			{
				await _amentityService.UpdateAsync(id, updateAmentity);
				return Ok("Updated");
			}
			catch (IncorrectFileFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (IncorrectIdException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete("{id}")]
		public async Task<ActionResult> Delete(int id)
		{
			try
			{
				await _amentityService.Delete(id);
				return Ok("Deleted");
			}
			catch(NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
	}
}
