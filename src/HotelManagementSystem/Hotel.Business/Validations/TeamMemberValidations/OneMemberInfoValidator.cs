
namespace Hotel.Business.Validations.TeamMemberValidations
{
	public class OneMemberInfoValidator:AbstractValidator<OneMemberInfoDto>
	{
		public OneMemberInfoValidator()
		{
			RuleFor(x => x.Fullname)
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
			    .NotEmpty()
			    .MaximumLength(70);
			RuleFor(x => x.Twitter)
				.NotEmpty()
				.MaximumLength(120);
			RuleFor(x => x.Facebook)
				.NotEmpty()
				.MaximumLength(120);
			RuleFor(x => x.Linkedin)
				.NotEmpty()
				.MaximumLength(120);
			RuleFor(x => x.Instagram)
				.NotEmpty()
				.MaximumLength(120);
		}
	}
}
