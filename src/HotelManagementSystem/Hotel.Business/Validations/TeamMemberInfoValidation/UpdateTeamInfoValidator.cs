namespace Hotel.Business.Validations.TeamMemberInfoValidation
{
	public class UpdateTeamInfoValidator:AbstractValidator<UpdateTeamMemberInfoDto>
	{
		public UpdateTeamInfoValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});
			RuleFor(x => x.Twitter)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120);
			RuleFor(x => x.Instagram)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120);
			RuleFor(x => x.Facebook)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120);
			RuleFor(x => x.Linkedin)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120);
			RuleFor(x => x.Phone)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
		}
	}
}
