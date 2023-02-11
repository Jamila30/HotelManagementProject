using Hotel.Business.DTOs.AmentityDTOs;
using Hotel.Business.DTOs.FlatAmentityDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlatAmentityController : ControllerBase
	{
		private readonly IFlatAmentityService _flatAmentityService;
		public FlatAmentityController(IFlatAmentityService flatAmentityService)
		{
			_flatAmentityService = flatAmentityService;
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			try
			{
				var list = await _flatAmentityService.GetAllAsync();
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
				var list = await _flatAmentityService.GetByIdAsync(id);
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

		[HttpGet("searchForFlatId/{flatId}")]
		public async Task<ActionResult> GetByFlatId(int flatId)
		{
			try
			{
				var list = await _flatAmentityService.GetByCondition(x => x.FlatId == flatId);
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

		[HttpGet("searchForAmentityId/{amentityId}")]

		public async Task<ActionResult> GetByAmentityId(int amentityId)
		{
			try
			{
				var list = await _flatAmentityService.GetByCondition(x => x.AmentityId == amentityId);
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
		public async Task<ActionResult> Post([FromForm] CreateFlatAmentityDto createFlatAmentity)
		{
			try
			{
				await _flatAmentityService.Create(createFlatAmentity);
				return Ok("Created");
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

		[HttpPut("{id}")]
		public async Task<ActionResult> Put(int id, [FromForm] UpdateFlatAmentityDto updateFlatAmentity)
		{
			try
			{
				await _flatAmentityService.UpdateAsync(id, updateFlatAmentity);
				return Ok("Updated");
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
				await _flatAmentityService.Delete(id);
				return Ok("Deleted");
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

	}


}

