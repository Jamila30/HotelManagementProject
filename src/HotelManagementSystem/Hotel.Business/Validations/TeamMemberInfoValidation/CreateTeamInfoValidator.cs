﻿namespace Hotel.Business.Validations.TeamMemberInfoValidation
{
	public class CreateTeamInfoValidator : AbstractValidator<CreateTeamInfoDto>
	{
		public CreateTeamInfoValidator()
		{
			RuleFor(x => x.TeamMemberId).NotEmpty().NotNull().Custom((Id, context) =>
			{
				if(!int.TryParse(Id.ToString(),out int id))
				{
					context.AddFailure("Enter valid format");
				}
			});
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
		}
	}
}
