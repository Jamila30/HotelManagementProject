namespace Hotel.Business.Validations.CommentValidations
{
	public class UpdateCommentValidator : AbstractValidator<UpdateCommentDto>
	{
		public UpdateCommentValidator()
		{
			RuleFor(c => c.Id)
				.Custom((Id, context) =>
				{
					if (!int.TryParse(Id.ToString(), out int id))
					{
						context.AddFailure("Enter Valid Format");
					}
				})
				 .NotNull()
				 .NotEmpty();
			RuleFor(c => c.Name)
			     .NotNull()
			     .MaximumLength(70)
			     .NotEmpty();
			RuleFor(c => c.Email)
				 .NotNull()
				 .NotEmpty()
				 .MaximumLength(70)
				 .EmailAddress();
			RuleFor(c => c.FlatId)
				.Custom((Id, context) =>
				{
					if (!int.TryParse(Id.ToString(), out int id))
					{
						context.AddFailure("Enter Valid Format");
					}
				})
				 .NotNull()
				 .NotEmpty();
			RuleFor(c => c.Opinions)
				 .NotNull()
				 .NotEmpty();
		}
	}
}
