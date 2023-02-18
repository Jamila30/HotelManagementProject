using Hotel.Business.DTOs.UserInfoDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserInfoController : ControllerBase
	{

		private readonly IUserInfoService _userInfoService;
		public UserInfoController(IUserInfoService userInfoService)
		{
			_userInfoService = userInfoService;
		}


		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			try
			{
				var list = await _userInfoService.GetAllAsync();
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
				var element = await _userInfoService.GetByIdAsync(id);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpGet("searchByEmail/{email}")]
		public async Task<IActionResult> GetByEmail(string email)
		{
			try
			{
				var element = await _userInfoService.GetByCondition(x => x.Email == email);
				return Ok(element);
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
				var element = await _userInfoService.GetByCondition(x => x.UserId == userId);
				return Ok(element);
			}
			catch (Exception ex)
			{
				return NotFound(ex.Message);
			}
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromForm] CreateUserInfoDto userInfoDto)
		{
			try
			{
				await _userInfoService.Create(userInfoDto);
				return Ok("Created");
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

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(int id, [FromForm] UpdateUserInfoDto userInfoDto)
		{
			try
			{
				await _userInfoService.UpdateAsync(id, userInfoDto);
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
				await _userInfoService.Delete(id);
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
