using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReservationController : ControllerBase
	{
		private readonly IReservationService _reservationService;
		public ReservationController(IReservationService reservationService)
		{
			_reservationService = reservationService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _reservationService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("searchByUserId/{userId}")]
		public async Task<IActionResult> GetByUserId(string userId)
		{
			try
			{
				var list = await _reservationService.GetByCondition(x => x.UserId == userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByFlatId/{flatId}")]
		public async Task<IActionResult> GetByFlatId(int flatId)
		{
			try
			{
				var list = await _reservationService.GetByCondition(x => x.FlatId == flatId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("searchByReservId/{reservId}")]
		public async Task<IActionResult> GetById(int reservId)
		{
			try
			{
				var list = await _reservationService.GetByIdAsync(reservId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpPost("getTotalPrice")]
		public async Task<IActionResult> TotalPrice(List<StabilPropertirsDto> reservIds)
		{
			try
			{
				var total = await _reservationService.GetTotalPrice(reservIds);
				return Ok(total);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
		
		[HttpPost("CanReserve/{flatId}")]
		public async Task<IActionResult> CanReserve(int flatId, DateDto date)
		{
			try
			{
				var result=await _reservationService.IsReserve(flatId, date);
				return Ok(result);
			}
			catch(NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("AvailableFlatsForReserve")]
		public async Task<IActionResult> AvailableFlatsForReserve(DateDto date)
		{
			try
			{
				var result = await _reservationService.AvailableFlatsForReserve(date);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPost("RecomendedFlats")]
		public async Task<IActionResult> RecomendedFlats(DateDto dateDto, int adults = 1, int children = 0)
		{
			try
			{
				var result = await _reservationService.RecomendedFlats(dateDto,adults,children);
				return Ok(result);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}


		[HttpGet("FinishEndedReservations")]
		public async Task<IActionResult> FinishEndedReservations()
		{
			try
			{
				 await _reservationService.FinishEndedReservations();
				return Ok("Done");
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}
		[HttpPost("CreateReserv")]
		public async Task<IActionResult> CreateReserv(DateTime CheckInDate, DateTime CheckOutDate, string UserId, [FromBody] List<CreateReservationDto> entities)
		{
			try
			{
				await _reservationService.CreateRezerv(CheckInDate,CheckOutDate, UserId, entities);
				return Ok("Created");
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
				throw;
				//return StatusCode(500);
			}
		}
		[HttpPut("CancelReservation")]
		public async Task<IActionResult> CancelReserv(int reservId)
		{
			try
			{
				await _reservationService.CancelReservation(reservId);
				return Ok("cancelled");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpPut("UpdateReserv/{Id}")]
		public async Task<IActionResult> Update(int Id, [FromForm] UpdateReservationDto updateReserv)
		{
			try
			{
				await _reservationService.UpdateAsync(Id, updateReserv);
				return Ok("Updated");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (IncorrectIdException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpDelete("{reservId}")]
		public async Task<IActionResult> Delete(int reservId)
		{
			try
			{
				await _reservationService.Delete(reservId);
				return Ok("Deleted reservation with this id");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

	}
}
