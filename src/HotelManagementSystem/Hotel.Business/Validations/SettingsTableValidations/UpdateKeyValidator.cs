namespace Hotel.Business.Validations.SettingsTableValidations
{
	public class UpdateKeyValidator:AbstractValidator<UpdateKeyDto>
	{
		public UpdateKeyValidator()
		{
			RuleFor(x => x.OldKey).NotNull().NotEmpty();
			RuleFor(x => x.NewKey).NotNull().NotEmpty();
		}
	}
}
