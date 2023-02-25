namespace Hotel.Core.Entities
{
	public class SettingsTable:IEntity
	{
		public int Id { get; set; }
		public string Key { get; set; } = null!;
		public string Value { get; set; } = null!;
	}
}
