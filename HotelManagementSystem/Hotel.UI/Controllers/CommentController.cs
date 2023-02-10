using Hotel.Business.DTOs.CommentDTOs;
using Hotel.Business.DTOs.FlatDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentController : ControllerBase
	{
		private readonly ICommentService _commentService;
		public CommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _commentService.GetAllAsync();
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
				var comment = await _commentService.GetByIdAsync(id);
				return Ok(comment);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpGet("searchEmail/{email}")]
		public async Task<IActionResult> GetByEmail(string email)
		{
			try
			{
				
				var comment = await _commentService.GetByEmail(email);
				return Ok(comment);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}


		[HttpGet("searchForFlatId/{id}")]
		public async Task<IActionResult> GetByFlatId(int id)
		{
			try
			{
				var comment = await _commentService.GetByCondition(x=>x.FlatId==id);
				return Ok(comment);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateCommentDto createComment)
		{
			try
			{
				await _commentService.Create(createComment);
				return Ok("Created");
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


		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm]UpdateCommentDto updateComment)
		{
			try
			{
				await _commentService.UpdateAsync(id, updateComment);
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
				await _commentService.Delete(id);
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
