using Hotel.Business.DTOs.SettingTableDTOs;

namespace Hotel.Business.Services.Implementations
{
	public class SettingsService : ISettingsService
	{
		private readonly ISettingTableRepository _repository;
		private readonly IMapper _mapper;
		public SettingsService(ISettingTableRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public async Task<Dictionary<string, SettingsTable>> GetAllAsync()
		{

			//Dictionary<string, string> dictionary = new Dictionary<string, string>();
			//var list = await _repository.GetAll().ToListAsync();
			//foreach (var item in list)
			//{
			//	dictionary.Add(item.Key, item.Value);
			//}
			//return dictionary;

			var list = await _repository.GetAll().ToListAsync();
			Dictionary<string, SettingsTable> dictionary = list.ToDictionary(x => x.Key);

			return dictionary;

		}

		public async Task<Dictionary<string, SettingsTable>> GetByCondition(Expression<Func<SettingsTable, bool>> expression)
		{
			var list = await _repository.GetAll().Where(expression).ToListAsync();
			Dictionary<string, SettingsTable> dictionary = list.ToDictionary(x => x.Key);

			return dictionary;


		}
		public async Task<List<string>> AllKey()
		{
			List<string> allKeys = new List<string>();
			var list = await _repository.GetAll().ToListAsync();
			Dictionary<string, SettingsTable> dictionary = list.ToDictionary(x => x.Key);
			Dictionary<string, SettingsTable>.KeyCollection keys = dictionary.Keys;
			foreach (var key in keys)
			{
				allKeys.Add(key);
			}
			return allKeys;
		}

		public async Task<List<string>> AllValues()
		{
			List<string> allValues = new List<string>();
			var list = await _repository.GetAll().ToListAsync();
			Dictionary<string, SettingsTable> dictionary = list.ToDictionary(x => x.Key);
			Dictionary<string, SettingsTable>.ValueCollection values = dictionary.Values;
			foreach (var settings in values)
			{
				allValues.Add(settings.Value);
			}
			return allValues;
		}
		public async Task Create(DictionaryDto dictionaryDto)
		{
			SettingsTable setting = new();
			if (dictionaryDto.Key != null && dictionaryDto.Value != null)
			{
				 setting = new SettingsTable() { Key = dictionaryDto.Key, Value = dictionaryDto.Value };
			}
			await _repository.Create(setting);
			await _repository.SaveChanges();
		}

		public async Task UpdateValueAsync(string key, DictionaryDto dictionaryDto)
		{
			if (key != dictionaryDto.Key) throw new BadRequestException("key must be same");
			var setting = await _repository.GetAll().SingleOrDefaultAsync(x => x.Key == key);
			if (setting is null) throw new NotFoundException("there is not this key");
			setting.Value = dictionaryDto.Value;
			_repository.Update(setting);
			await _repository.SaveChanges();
		}

		public async Task Delete(int id)
		{
			var setting = await _repository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
			if (setting is null) throw new NotFoundException("there is not setting with this id ");
			_repository.Delete(setting);
			await _repository.SaveChanges();
		}

	}
}
