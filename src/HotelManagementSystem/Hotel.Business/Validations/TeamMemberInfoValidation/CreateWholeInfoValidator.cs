namespace Hotel.Business.Validations.TeamMemberInfoValidation
{
	public class CreateWholeInfoValidator:AbstractValidator<CreateWholeMemberDto>
	{
		public CreateWholeInfoValidator()
		{
			RuleFor(x => x.Phone)
			    .NotEmpty()
			    .NotNull()
			    .MaximumLength(70);
			RuleFor(x => x.Twitter)
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
			RuleFor(x => x.Instagram)
				.NotEmpty()
				.NotNull()
				.MaximumLength(120);
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
