namespace Hotel.Business.Validations.AccountValidations
{
	public class JustEmailValidator:AbstractValidator<JustEmailDto>
	{
		public JustEmailValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull()
				.EmailAddress();
		}
	}
}
