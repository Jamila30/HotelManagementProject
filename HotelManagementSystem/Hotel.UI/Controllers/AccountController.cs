using Hotel.Business.DTOs.AccountsDTOs;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		private readonly IAccountService _accountService;
		public AccountController(IAccountService accountService)
		{
			_accountService = accountService;
		}

		[HttpGet]
		public async Task<ActionResult> Get()
		{
			try
			{
				var accounts = await _accountService.GetAllAccounts();
				return Ok(accounts);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpGet("{userId}")]
		public async Task<ActionResult> GetById(string userId)
		{
			try
			{
				var account = await _accountService.GetAccount(userId);
				return Ok(account);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost]
		public async Task<ActionResult> CreateAccount(CreateAccountDto createAccount)
		{
			try
			{
				await _accountService.CreateAccount(createAccount);
				return Ok("Created");
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("Add Role to User")]
		public async Task<ActionResult> AddUserRole(UserRoleDto userDto)
		{
			try
			{
				await _accountService.AddUserRole(userDto);
				return Ok("Added this role to user's roles");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}
		[HttpPut("Update Account")]
		public async Task<ActionResult> UpdateAccount(string userId, UpdateUserDto updateUserDto)
		{
			try
			{
				await _accountService.UpdateAccount(userId, updateUserDto);
				return Ok("Updated");
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

		[HttpPut("Block Account")]
		public async Task<ActionResult> BlockAccount(BlockAccountDto blockAccount)
		{
			try
			{
				await _accountService.BlockAccount(blockAccount);
				return Ok("Account Blocked");
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

		[HttpPut("Unblock Account")]
		public async Task<ActionResult> UnBlockAccount(JustEmailDto blockEmail)
		{
			try
			{
				await _accountService.UnBlockAccount(blockEmail);
				return Ok("Account UnBlocked");
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

		[HttpPut("Update User's Role")]
		public async Task<ActionResult> UpdateUserRole(UpdateUserRolesDto updateUserRoles)
		{
			try
			{
				await _accountService.UpdateUserRole(updateUserRoles);
				return Ok("Update role of User");
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

		[HttpDelete("Delete Account")]
		public async Task<ActionResult> DeleteAccount(JustEmailDto deleteAccount)
		{
			try
			{
				await _accountService.DeleteAccount(deleteAccount);
				return Ok("Deleted Account");
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

		[HttpDelete("Delete Role")]
		public async Task<ActionResult> DeleteUserRole(UserRoleDto deleteRole)
		{
			try
			{
				await _accountService.DeleteUserRole(deleteRole);
				return Ok("this user's role deleted ");
			}
			catch (NotFoundException ex)
			{
				return NotFound(ex.Message);
			}
			catch (AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}
	}
}
