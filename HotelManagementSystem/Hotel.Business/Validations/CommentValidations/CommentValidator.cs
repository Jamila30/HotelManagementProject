namespace Hotel.Business.Validations.CommentValidations
{
	public class CommentValidator:AbstractValidator<CommentDto>
	{
		public CommentValidator()
		{
			RuleFor(c=>c.Id)
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
			RuleFor(c => c.Created)
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
