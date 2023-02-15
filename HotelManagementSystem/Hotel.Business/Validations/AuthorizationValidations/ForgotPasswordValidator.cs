using Hotel.Business.DTOs.AuthorizationDTOs;

namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class ForgotPasswordValidator:AbstractValidator<ForgotPasswordDto>
	{
		public ForgotPasswordValidator()
		{
			RuleFor(x => x.EmailOrUsername)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120);
		}
	}
}
