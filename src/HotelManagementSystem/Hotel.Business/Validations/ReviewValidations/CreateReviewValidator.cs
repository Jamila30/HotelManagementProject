namespace Hotel.Business.Validations.ReviewValidations
{
	public class CreateReviewValidator:AbstractValidator<CreateReviewDto>
	{
		public CreateReviewValidator()
		{
			RuleFor(r => r.UserId).NotNull().NotEmpty();
			RuleFor(r => r.FlatId).NotNull().NotEmpty();
			RuleFor(r => r.Rate).LessThanOrEqualTo(5).GreaterThanOrEqualTo(1).NotNull().NotEmpty();
			RuleFor(r => r.Opinions).MaximumLength(300).NotNull().NotEmpty();
		}
	}
}
