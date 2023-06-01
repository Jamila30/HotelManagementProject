using Microsoft.AspNetCore.Authorization;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamMemberController : ControllerBase
	{
		private readonly ITeamMemberService _memberService;
		public TeamMemberController(ITeamMemberService memberService)
		{
			_memberService = memberService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _memberService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByFullname/{memberName}")]
		public async Task<IActionResult> GetByFullname(string memberName)
		{
			try
			{
				var member = await _memberService.GetByCondition(x => x.Fullname == memberName);
				return Ok(member);
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
				var member = await _memberService.GetByIdAsync(id);
				return Ok(member);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}
		[Authorize(Roles = "Admin")]
		[HttpPost("CreateTeamWithInfo")]
		public async Task<IActionResult> CreateTeamWithInfo([FromForm] CreateWholeMemberDto createWhole)
		{
			try
			{
				await _memberService.CreateTeamWithInfo(createWhole);
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
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPost]
		public async Task<IActionResult> Create([FromForm] CreateTeamMemberDto  createTeam)
		{
			try
			{
				await _memberService.Create(createTeam);
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
		public async Task<IActionResult> Put(int id, [FromForm] UpdateTeamMemberDto updateTeam)
		{
			try
			{
				await _memberService.UpdateAsync(id, updateTeam);
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
				await _memberService.Delete(id);
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
