namespace Hotel.Business.Validations.AccountValidations
{
	public class UpdateUserRoleValidator:AbstractValidator<UpdateUserRolesDto>
	{
		public UpdateUserRoleValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.NotNull()
				.EmailAddress();
			RuleFor(x => x.OldRoleName)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.NewRoleName)
				.NotEmpty()
				.NotNull();
		}
	}
}
