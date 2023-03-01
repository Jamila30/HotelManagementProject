namespace Hotel.Business.Validations.ServiceImageValidations
{
	public class ServiceImageValidator:AbstractValidator<ServiceImageDto>
	{
		public ServiceImageValidator()
		{
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
		}
	}
}
