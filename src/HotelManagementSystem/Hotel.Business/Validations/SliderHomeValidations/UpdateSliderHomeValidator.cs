

namespace Hotel.Business.Validations.SliderHomeValidations
{
	public class UpdateSliderHomeValidator:AbstractValidator<UpdateSliderHomeDto>
	{
		public UpdateSliderHomeValidator()
		{
			RuleFor(x => x.Id).Custom((Id, contex) =>
			{
				if(!int.TryParse(Id.ToString(),out int id))
				{
					contex.AddFailure("Enter true Id format");
				}
			});

			RuleFor(x => x.Title)
				.NotEmpty()
				.NotNull()
				.MaximumLength(70);
			RuleFor(x => x.Description)
				.NotEmpty()
				.NotNull()
				.MaximumLength(250);
			
		}
	}
}
