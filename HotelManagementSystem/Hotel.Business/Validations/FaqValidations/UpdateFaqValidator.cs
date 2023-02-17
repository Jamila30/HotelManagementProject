using Hotel.Business.DTOs.FaqDTOs;

namespace Hotel.Business.Validations.FaqValidations
{
	public class UpdateFaqValidator : AbstractValidator<UpdateFaqDto>
	{
		public UpdateFaqValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty()
				.NotNull()
				.Custom((Id, context) =>
			{
				if (!int.TryParse(Id.ToString(), out int id))
				{
					context.AddFailure("Enter Valid Format");
				}
			});
			RuleFor(x => x.Question).NotNull().NotEmpty();
			RuleFor(x => x.Answer).NotNull().NotEmpty();
		}
	}
}
