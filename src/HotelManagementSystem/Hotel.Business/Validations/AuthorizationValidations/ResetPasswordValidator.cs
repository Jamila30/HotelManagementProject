namespace Hotel.Business.Validations.AuthorizationValidations
{
	public class ResetPasswordValidator : AbstractValidator<ResetPasswordDto>
	{
		public ResetPasswordValidator()
		{
			RuleFor(x => x.NewPassword)
			    .NotEmpty()
			    .NotNull();
			RuleFor(x => x.NewConfirmedPassword)
				.NotEmpty()
				.NotNull()
				.Equal(x => x.NewPassword).WithMessage("Confirmed Password must be same with Password");
		}
	}
}
