using Hotel.Business.DTOs.ServiceImageDTOs;

namespace Hotel.Business.Validations.ServiceImageValidations
{
	public class CreateServiceImagevalidator:AbstractValidator<CreateServiceImageDto>
	{
		public CreateServiceImagevalidator()
		{
			RuleFor(x => x.Image)
				.NotEmpty()
				.NotNull();
		
		}
	}
}
