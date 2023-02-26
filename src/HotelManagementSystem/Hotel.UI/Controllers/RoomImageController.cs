using Hotel.Business.DTOs.GallaryImageDTOs;
using Hotel.Business.DTOs.RoomImageDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoomImageController : ControllerBase
	{
		private readonly IRoomImageService _roomImageService;
		public RoomImageController(IRoomImageService roomImageService)
		{
			_roomImageService = roomImageService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _roomImageService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}


		[HttpGet("searchByFlatId/{id}")]
		public async Task<IActionResult> GetByCatagoryId(int id)
		{
			try
			{
				var element = await _roomImageService.GetByCondition(x => x.FlatId == id);
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

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(int id)
		{
			try
			{
				var element = await _roomImageService.GetByIdAsync(id);
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

		[HttpPost]

		public async Task<IActionResult> Create([FromForm] CreateRoomImageDto createRoomImage)
		{
			try
			{

				await _roomImageService.Create(createRoomImage);
				return Ok("Created");
			}
			catch (IncorrectFileSizeException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (RepeatedImageException ex)
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
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPut]

		public async Task<IActionResult> Put(int id, [FromForm] UpdateRoomImageDto updateRoom)
		{
			try
			{
				await _roomImageService.UpdateAsync(id, updateRoom);
				return Ok("Updated");
			}
			catch (IncorrectIdException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (IncorrectFileSizeException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (IncorrectFileFormatException ex)
			{

				return BadRequest(ex.Message);
			}
			catch (RepeatedImageException ex)
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
				await _roomImageService.Delete(id);
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
