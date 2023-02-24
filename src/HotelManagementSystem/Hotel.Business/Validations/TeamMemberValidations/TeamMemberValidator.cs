
namespace Hotel.Business.Validations.TeamMemberValidations
{
	public class TeamMemberValidator:AbstractValidator<TeamMemberDto>
	{
		public TeamMemberValidator()
		{
			RuleFor(x=>x.Fullname)
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
