using Hotel.Business.DTOs.AuthorizationDTOs;

namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class MailRequestValidator : AbstractValidator<MailRequestDto>
	{
		public MailRequestValidator()
		{
			RuleFor(x => x.Subject)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Body)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.ToEmail)
				.NotEmpty()
				.NotNull();
		}
	}
}
