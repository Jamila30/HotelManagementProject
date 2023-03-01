
namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoomCatagoryController : ControllerBase
	{
		private readonly IRoomCatagoryService _catagoryService;
		public RoomCatagoryController(IRoomCatagoryService catagoryService)
		{
			_catagoryService = catagoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _catagoryService.GetAllAsync();
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
				var element = await _catagoryService.GetByIdAsync(id);
				return Ok(element);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByCatagoryName/{name}")]
		public async Task<IActionResult> GetByCatagoryName(string name)
		{
			try
			{
				var element = await _catagoryService.GetByCondition(x => x.Name == name);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post( CreateRoomCatagoryDto createCatagory)
		{
			try
			{
				await _catagoryService.Create(createCatagory);
				return Ok("Created");
			}
			catch (RepeatedSameCatagoryNameException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, UpdateRoomCatagoryDto updateCatagory)
		{
			try
			{
				await _catagoryService.UpdateAsync(id, updateCatagory);
				return Ok("Updated");
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}
			catch (IncorrectIdException ex)
			{

				return NotFound(ex.Message);
			}
			catch (RepeatedSameCatagoryNameException ex)
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
				await _catagoryService.Delete(id);
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
