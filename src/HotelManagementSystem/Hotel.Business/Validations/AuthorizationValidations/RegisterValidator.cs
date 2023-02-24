using Hotel.Business.DTOs.AuthorizationDTOs;

namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class RegisterValidator : AbstractValidator<RegisterDto>
	{
		public RegisterValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120)
				.MinimumLength(5)
				.EmailAddress();
			RuleFor(x => x.Password)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.ConfirmedPassword)
				.NotEmpty()
				.NotNull()
				.Equal(x => x.Password).WithMessage("Must be same with Password");
			RuleFor(x => x.Fullname)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120)
				.MinimumLength(4);
			RuleFor(x => x.Username)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120)
				.MinimumLength(4);
		}
	}
}
