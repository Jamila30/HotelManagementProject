namespace Hotel.Business.Validations.AccountValidations
{
	public class CreateAccountValidator:AbstractValidator<CreateAccountDto>
	{
		public CreateAccountValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(256);
			RuleFor(x => x.Email).NotEmpty().NotNull().MaximumLength(256);
			RuleFor(x => x.FullName).NotEmpty().NotNull();
			RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
			RuleFor(x => x.Password).NotEmpty().NotNull();
			RuleFor(x => x.ConfirmedPassword).NotEmpty().NotNull().Equal(x => x.Password).WithMessage("Password and confirmed password must be same");

		}
	}
}
