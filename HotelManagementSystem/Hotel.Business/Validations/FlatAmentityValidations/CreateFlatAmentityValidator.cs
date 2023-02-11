namespace Hotel.Business.Validations.FlatAmentityValidations
{
	public class CreateFlatAmentityValidator:AbstractValidator<CreateFlatAmentityDto>
	{
		public CreateFlatAmentityValidator()
		{
			RuleFor(x => x.FlatId)
				.NotNull()
				.NotEmpty();
			RuleFor(x => x.AmentityId)
				.NotNull()
				.NotEmpty();
		}
	}
}
