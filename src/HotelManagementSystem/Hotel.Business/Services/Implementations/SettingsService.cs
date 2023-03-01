namespace Hotel.Business.Services.Implementations
{
	public class SettingsService : ISettingsService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public SettingsService(IMapper mapper, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_unitOfWork = unitOfWork;
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

			var list = await _unitOfWork.settingTableRepository.GetAll().ToListAsync();
			Dictionary<string, SettingsTable> dictionary = list.ToDictionary(x => x.Key);
			return dictionary;
		}

		public async Task<Dictionary<string, SettingsTable>> GetByCondition(Expression<Func<SettingsTable, bool>> expression)
		{
			var list = await _unitOfWork.settingTableRepository.GetAll().Where(expression).ToListAsync();
			Dictionary<string, SettingsTable> dictionary = list.ToDictionary(x => x.Key);

			return dictionary;


		}
		public async Task<List<string>> AllKey()
		{
			List<string> allKeys = new List<string>();
			var list = await _unitOfWork.settingTableRepository.GetAll().ToListAsync();
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
			var list = await	_unitOfWork.settingTableRepository.GetAll().ToListAsync();
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
			await _unitOfWork.settingTableRepository.Create(setting);
			await _unitOfWork.SaveAsync();
		}

		public async Task UpdateValueAsync(string key, DictionaryDto dictionaryDto)
		{
			if (key != dictionaryDto.Key) throw new BadRequestException("key must be same");
			var setting = await _unitOfWork.settingTableRepository.GetAll().SingleOrDefaultAsync(x => x.Key == key);
			if (setting is null) throw new NotFoundException("there is not this key");
			setting.Value = dictionaryDto.Value;
			_unitOfWork.settingTableRepository.Update(setting);
			await _unitOfWork.SaveAsync();
		}
		public async Task UpdateKeyAsync(UpdateKeyDto updateKey)
		{
			var setting = await _unitOfWork.settingTableRepository.GetAll().FirstOrDefaultAsync(x => x.Key == updateKey.OldKey);
			if (setting is null) throw new NotFoundException("there is not this key");
			setting.Key = updateKey.NewKey;
			_unitOfWork.settingTableRepository.Update(setting);
			await _unitOfWork.SaveAsync();
		}
		public async Task Delete(int id)
		{
			var setting = await _unitOfWork.settingTableRepository.GetAll().FirstOrDefaultAsync(x => x.Id == id);
			if (setting is null) throw new NotFoundException("there is not setting with this id ");
			_unitOfWork.settingTableRepository.Delete(setting);
			await _unitOfWork.SaveAsync();
		}

	}
}
