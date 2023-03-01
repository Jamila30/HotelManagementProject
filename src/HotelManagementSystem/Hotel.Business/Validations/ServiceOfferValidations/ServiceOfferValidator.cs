namespace Hotel.Business.Validations.ServiceOfferValidations
{
	public class ServiceOfferValidator:AbstractValidator<ServiceOfferDto>
	{
		public ServiceOfferValidator()
		{
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
