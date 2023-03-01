namespace Hotel.Business.Validations.AccountValidations
{
	public class UserRoleValidator:AbstractValidator<UserRoleDto>
	{
		public UserRoleValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.RoleName)
				.NotEmpty()
				.NotNull();
		}
	}
}
