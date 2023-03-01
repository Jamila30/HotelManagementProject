using Hotel.Business.DTOs.SettingTableDTOs;

namespace Hotel.Business.Services.Interfaces
{
	public interface ISettingsService
	{

		Task<Dictionary<string, SettingsTable>> GetAllAsync();
		Task<Dictionary<string, SettingsTable>> GetByCondition(Expression<Func<SettingsTable, bool>> expression);
		Task<List<string>> AllKey();
		Task<List<string>> AllValues();
		Task Create(DictionaryDto dictionaryDto);
		Task UpdateValueAsync(string key, DictionaryDto dictionaryDto);
		Task UpdateKeyAsync(UpdateKeyDto updateKey);
		Task Delete(int id);
	}
}
