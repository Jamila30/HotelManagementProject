using Hotel.Business.DTOs.ReviewDTOs;
using Hotel.Business.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReviewController : ControllerBase
	{

		private readonly IReviewService _reviewService;
		public ReviewController(IReviewService reviewService)
		{
			_reviewService = reviewService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _reviewService.GetAllAsync();
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
				var list = await _reviewService.GetByCondition(r=>r.UserId==userId);
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByFlatId/{flatId}")]
		public async Task<IActionResult> GetByUserId(int flatId)
		{
			try
			{
				var list = await _reviewService.GetByCondition(r => r.FlatId == flatId);
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
				var question = await _reviewService.GetByIdAsync(id);
				return Ok(question);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpPost]
		public async Task<IActionResult> AddReview(CreateReviewDto createReview)
		{
			try
			{
				await _reviewService.Create(createReview);
				return Ok("Created");
			}
			catch(NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch(AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id,UpdateReviewDto review)
		{
			try
			{
				await _reviewService.Update(id,review);
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
				await _reviewService.Delete(id);
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
