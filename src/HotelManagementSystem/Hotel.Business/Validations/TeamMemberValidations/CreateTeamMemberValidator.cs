namespace Hotel.Business.Validations.TeamMemberValidations
{
	public class CreateTeamMemberValidator:AbstractValidator<CreateTeamMemberDto>
	{
		public CreateTeamMemberValidator()
		{
			RuleFor(x => x.Fullname)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Position)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Image)
				.NotNull()
				.NotEmpty();
		}
	}
}
