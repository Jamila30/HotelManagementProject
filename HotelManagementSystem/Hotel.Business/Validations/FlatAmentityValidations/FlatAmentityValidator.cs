namespace Hotel.Business.Validations.FlatAmentityValidations
{
	public class FlatAmentityValidator:AbstractValidator<FlatAmentityDto>
	{
		public FlatAmentityValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.NotNull();
			RuleFor(x => x.FlatId)
				.NotNull()
				.NotEmpty();
			RuleFor(x => x.AmentityId)
				.NotNull()
				.NotEmpty();
		}
	}
}
