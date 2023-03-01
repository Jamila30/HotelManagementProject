namespace Hotel.Business.Services.Interfaces
{
	public interface ISelectedListService
	{
		Task<List<SelectedListDto>> GetAllAsync();
		Task<List<SelectedListDto>> GetByCondition(Expression<Func<SelectedList, bool>> expression);
		Task<SelectedListDto?> GetByIdAsync(int id);
		Task AddToList(List<int> flatIds);
		Task UpdateAsync(int catagoryId, UpdateSelectedListDto updateList);
		Task DeleteOfOneCatagory(int catagoryId);
		Task<float> GetTotalPrice();
		Task DeleteAllListItems();
	}
}
