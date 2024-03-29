﻿namespace Hotel.Business.Validations.CommentValidations
{
	public class CreateCommentValidator : AbstractValidator<CreateCommentDto>
	{
		public CreateCommentValidator()
		{

			RuleFor(c => c.Name)
				.NotNull()
				.MaximumLength(70)
				.NotEmpty();
			RuleFor(c => c.UserId)
				.NotNull()
				.NotEmpty();
			RuleFor(c => c.FlatId)
					.NotNull()
					.NotEmpty();
			RuleFor(c => c.Opinions)
				.NotNull()
				.NotEmpty();
		}
	}
}
