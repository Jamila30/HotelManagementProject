namespace Hotel.Business.Validations.AccountValidations
{
	public class UpdateUserValidator:AbstractValidator<UpdateUserDto>
	{
		public UpdateUserValidator()
		{
			RuleFor(x => x.UserName).NotEmpty().NotNull().MaximumLength(256);
			RuleFor(x => x.Email).NotEmpty().NotNull().MaximumLength(256);
			RuleFor(x => x.FullName).NotEmpty().NotNull();
			RuleFor(x => x.PhoneNumber).NotEmpty().NotNull();
			RuleFor(x => x.Id).NotEmpty().NotNull();
		}
	}
}
