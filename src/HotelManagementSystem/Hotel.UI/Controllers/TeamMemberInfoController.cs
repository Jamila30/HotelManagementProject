using Hotel.Business.Utilities.Enums;
using Microsoft.AspNetCore.Authorization;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class TeamMemberInfoController : ControllerBase
	{
		private readonly ITeamMemberInfoService _memberInfoService;
		public TeamMemberInfoController(ITeamMemberInfoService memberInfoService)
		{
			_memberInfoService = memberInfoService;
		}

		[HttpGet("")]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _memberInfoService.GetAllAsync();
				return Ok(list);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}
		[HttpGet("searchByPhone/{phone}")]
		public async Task<IActionResult> GetByPhone(string phone)
		{
			try
			{
				var member = await _memberInfoService.GetByCondition(x => x.Phone == phone);
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
				var member = await _memberInfoService.GetByIdAsync(id);
				return Ok(member);
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}

		}

		[Authorize(Roles = "Admin")]
		[HttpPost("createInfoForMember/{memberId}")]
		public async Task<IActionResult> CreateInfoForExistMember(int memberId,CreateTeamInfoDto createTeam)
		{
			try
			{
				await _memberInfoService.CreateInfoForExistMember(memberId, createTeam);
				return Ok("Created");
			}
			catch (IncorrectFormatException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (NotFoundException ex)
			{
				return BadRequest(ex.Message);
			}
			catch(IncorrectIdException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[Authorize(Roles = "Admin")]
		[HttpPost("createInfoWithNewMember")]
		public async Task<IActionResult> CreateInfoWithNewMember([FromForm]CreateWholeInfoDto createWhole)
		{
			try
			{
				await _memberInfoService.CreateInfoWithNewMember(createWhole);
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
			catch (NotFoundException ex)
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
		public async Task<IActionResult> Put(int id, [FromForm] UpdateTeamMemberInfoDto updateTeam)
		{
			try
			{
				await _memberInfoService.UpdateAsync(id, updateTeam);
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
				await _memberInfoService.Delete(id);
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
