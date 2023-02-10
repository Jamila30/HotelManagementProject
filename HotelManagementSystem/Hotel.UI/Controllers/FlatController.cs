using Hotel.Business.DTOs.FlatDTOs;
using Hotel.Business.Validations.FlatValidations;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class FlatController : ControllerBase
	{
		private readonly IFlatService _flatService;
		public FlatController(IFlatService flatService)
		{
			_flatService = flatService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _flatService.GetAllAsync();
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
				var slider = await _flatService.GetByIdAsync(id);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}
		[HttpGet("untilPrice/{price}")]
		public async Task<IActionResult> GetUntilPrice(float price)
		{
			try
			{
				var slider = await _flatService.GetByCondition(x=>x.Price<price);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpGet("searchBedCount/{count}")]
		public async Task<IActionResult> GetUntilPrice(int count)
		{
			try
			{
				var slider = await _flatService.GetByCondition(x => x.BedCount==count);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpGet("searchForAdultsCount/{fromCount}")]
		public async Task<IActionResult> GetByAdultsCount(int fromCount)
		{
			try
			{
				var slider = await _flatService.GetByCondition(x => x.Adults >=fromCount);
				return Ok(slider);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateFlatDto createFlat)
		{
			try
			{
				await _flatService.Create(createFlat);
				return Ok("Created");
			}
			catch (IncorrectIdException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm] UpdateFlatDto updateFlat)
		{
			try
			{
				await _flatService.UpdateAsync(id, updateFlat);
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
				await _flatService.Delete(id);
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
