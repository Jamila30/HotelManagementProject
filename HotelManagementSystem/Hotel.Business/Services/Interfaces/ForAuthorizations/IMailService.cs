using Hotel.Business.DTOs.AuthorizationDTOs;

namespace Hotel.Business.Services.Interfaces.ForAuthorizations
{
	public interface IMailService
	{
		Task SendEmailAsync(MailRequestDto mailRequest);
	}
}
