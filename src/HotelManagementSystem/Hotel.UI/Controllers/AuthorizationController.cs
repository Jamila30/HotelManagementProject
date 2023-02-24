using Hotel.Business.DTOs.AuthorizationDTOs;
using Hotel.Core.Entities;

namespace Hotel.UI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthorizationController : ControllerBase
	{

		private readonly IAutService _authorizationService;
		private readonly IMailService _mailService;
		public AuthorizationController(IAutService authorizationService, IMailService mailService)
		{
			_authorizationService = authorizationService;
			_mailService = mailService;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
		{
			try
			{

				var response = await _authorizationService.RegisterAsync(registerDto);

				var link = Url.Action("ConfirmEmail", "Authorization", new { userId = response.UserId, token = response.Token }, Request.Scheme);
				await _mailService.SendEmailAsync(new MailRequestDto()
				{
					ToEmail = response.Email,
					Subject = "Email Confirmation ",
					Body = $"<a href={link}>Click Here for Confirmation Now! </a>"
				});
				return Ok(response);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
			}
		}

		[HttpGet("[action]")]
		public async Task<IActionResult> ConfirmEmail(string token, string userId)
		{
			try
			{
				await _authorizationService.ConfirmEmail(token, userId);
				return Ok("Email confirmed by user");
			}
			catch (ConfirmationException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (AlreadyExistException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{

				throw;
			}
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Login([FromForm] LoginDto login)
		{
			try
			{
				var response = await _authorizationService.LoginAsync(login);
				return Ok(response);
			}
			catch (ConfirmationException ex)
			{
				return Unauthorized(ex.Message);

			}
			catch (NotFoundException ex)
			{
				return Unauthorized(ex.Message);
			}
			catch (Exception)
			{
				return StatusCode(500);
				
			}
		}

		[HttpPost("RefreshToken")]
		public async Task<IActionResult> RefreshToken(Tokens tokenModel)
		{
			try
			{
				var tokens = await _authorizationService.RefreshToken(tokenModel);
				return Ok(tokens);
			}
			catch (BadRequestException ex)
			{
				return BadRequest(ex.Message);
			}
			catch (Exception)
			{

				throw;
			}
		}
		[HttpPost("[action]")]
		public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordDto forgotPassword)
		{
			try
			{
				var response = await _authorizationService.ForgotPasswordAsync(forgotPassword);
				var link = Url.Action("ResetPassword", "Authorization", new { userId = response.UserId, token = response.Token }, Request.Scheme);
				await _mailService.SendEmailAsync(new MailRequestDto()
				{
					ToEmail = response.Email,
					Subject = "Email Confirmation ",
					Body = $"<a href={link}>Click Here for Confirmation Now! </a>"
				});
				//return Ok("Email send to user for reset password");
				return Ok(response);
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

		[HttpPost("[action]")]
		public async Task<IActionResult> ResetPasswordAsync(string token, string userId, [FromBody] ResetPasswordDto resetPassword)
		{
			try
			{
				await _authorizationService.ResetPasswordAsync(token, userId, resetPassword);
				return Ok("reset done");
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
				return StatusCode(500);
			}
		}
	}
}
