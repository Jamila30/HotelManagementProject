namespace Hotel.Business.Validations.UserInfoValidations
{
	public class UpdateUserInfoValidator:AbstractValidator<UpdateUserInfoDto>
	{
		public UpdateUserInfoValidator()
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
				.NotEmpty()
				.MaximumLength(70);
			RuleFor(x => x.Id)
				.NotNull()
				.NotEmpty().Custom((Id, contex) =>
				{
					if (!int.TryParse(Id.ToString(), out int id))
					{
						contex.AddFailure("Enter true Id format");
					}
				});
		
		}
	}
}
