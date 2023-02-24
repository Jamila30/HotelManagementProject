using Hotel.Business.DTOs.AuthorizationDTOs;

namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class TokenResponseValidator:AbstractValidator<TokenResponseDto>
	{
		public TokenResponseValidator()
		{
			RuleFor(x => x.Username)
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
