namespace Hotel.Business.Validations.ServiceOfferValidations
{
	public class UpdateServiceOfferValidator:AbstractValidator<UpdateServiceOfferDto>
	{
		public UpdateServiceOfferValidator()
		{
			RuleFor(x => x.Id).Custom((Id, context) =>
			{
				if(!int.TryParse(Id.ToString(),out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.Title)
			  .NotEmpty()
			  .NotNull()
			  .MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.Price)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.IsFree)
				.NotNull();
			


		}
	}
}
