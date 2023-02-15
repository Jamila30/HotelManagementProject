namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class LoginValidator:AbstractValidator<LoginDto>
	{
		public LoginValidator()
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
		}
	}
}
