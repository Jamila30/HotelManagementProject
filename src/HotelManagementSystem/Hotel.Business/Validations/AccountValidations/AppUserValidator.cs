namespace Hotel.Business.Validations.AccountValidations
{
	public class AppUserValidator:AbstractValidator<AppUserDto>
	{
		public AppUserValidator()
		{
			RuleFor(x=>x.Id).NotEmpty().NotNull().MaximumLength(450);
			RuleFor(x=>x.UserName).NotEmpty().NotNull().MaximumLength(256);
			RuleFor(x=>x.Email).NotEmpty().NotNull().MaximumLength(256);
			RuleFor(x=>x.FullName).NotEmpty().NotNull();
			RuleFor(x=>x.PhoneNumber).NotEmpty().NotNull();
			RuleFor(x => x.EmailConfirmed).NotNull();
			RuleFor(x => x.LockOutDate).NotEmpty();

		}
	}
}
