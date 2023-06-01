namespace Hotel.Business.Validations.ReviewValidations
{
	public class UpdateReviewValidator:AbstractValidator<UpdateReviewDto>
	{
		public UpdateReviewValidator()
		{
			RuleFor(r => r.Id).NotNull().NotEmpty().Custom((Id, context) =>
			{
				if(!int.TryParse(Id.ToString(),out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(r => r.Rate).LessThanOrEqualTo(5).GreaterThanOrEqualTo(1).NotNull().NotEmpty();
			RuleFor(r => r.Opinions).MaximumLength(300).NotNull().NotEmpty();
		}
	}
}
