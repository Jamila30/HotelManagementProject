namespace Hotel.Business.Validations.TeamMemberValidations
{
	public class UpdateTeamMemberValidator:AbstractValidator<UpdateTeamMemberDto>
	{
		public UpdateTeamMemberValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});
			RuleFor(x => x.Fullname)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Position)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Image);
				

		}
	}
}
