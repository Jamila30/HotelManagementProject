namespace Hotel.Business.Validations.TeamMemberValidations
{
	public class CreateWholeMemberValidator:AbstractValidator<CreateWholeMemberDto>
	{
		public CreateWholeMemberValidator()
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
			RuleFor(x => x.Phone)
				.NotNull()
				.NotEmpty()
			    .MaximumLength(70);
			RuleFor(x => x.Twitter)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Facebook)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Linkedin)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Instagram)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);

		}
	}
}
