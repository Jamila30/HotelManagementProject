namespace Hotel.Business.Validations.ReviewValidations
{
	public class ReviewValidator : AbstractValidator<ReviewDto>
	{
		public ReviewValidator()
		{
			RuleFor(r => r.Id).NotNull().NotEmpty();
			RuleFor(r => r.UserId).NotNull().NotEmpty();
			RuleFor(r => r.FlatId).NotNull().NotEmpty();
			RuleFor(r => r.Rate).NotNull().NotEmpty();
			RuleFor(r => r.Opinions).MaximumLength(300).NotNull().NotEmpty();

		}
	}
}
