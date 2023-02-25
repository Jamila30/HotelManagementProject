using Hotel.Business.DTOs.SettingTableDTOs;

namespace Hotel.Business.Validations.SettingsTableValidations
{
	public class DictionaryValidator:AbstractValidator<DictionaryDto>
	{
		public DictionaryValidator()
		{
			RuleFor(d => d.Key).NotEmpty().NotNull();
			RuleFor(d => d.Value).NotEmpty().NotNull();	
		}
	}
}
