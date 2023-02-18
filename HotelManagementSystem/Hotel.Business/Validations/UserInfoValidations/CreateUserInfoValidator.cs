using Hotel.Business.DTOs.UserInfoDTOs;

namespace Hotel.Business.Validations.UserInfoValidations
{
	public class CreateUserInfoValidator:AbstractValidator<CreateUserInfoDto>
	{
		public CreateUserInfoValidator()
		{
			RuleFor(x => x.LastName)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.FirstName)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Email)
				.NotNull()
				.NotEmpty()
				.MaximumLength(120)
				.EmailAddress();
			RuleFor(x => x.Address)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Phone)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.City)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Country)
				.NotNull()
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.PostCode)
				.NotNull()
				.NotEmpty();
		}
	}
}
