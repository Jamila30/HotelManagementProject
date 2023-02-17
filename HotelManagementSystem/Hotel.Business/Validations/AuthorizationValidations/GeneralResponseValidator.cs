using Hotel.Business.DTOs.AuthorizationDTOs;

namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class GeneralResponseValidator:AbstractValidator<GeneralResponseDto>
	{
		public GeneralResponseValidator()
		{
			RuleFor(x => x.Username)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull()
				.EmailAddress();
			RuleFor(x => x.UserId)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Token)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.ExpireDate)
				.NotEmpty()
				.NotNull();
		}
	}
}
