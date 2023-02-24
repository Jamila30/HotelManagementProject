namespace Hotel.DataAccess.Repositories.Implementations
{
	public class RoomCatagoryRepository : Repository<RoomCatagory>, IRoomCatagoryRepository
	{
		public RoomCatagoryRepository(AppDbContext context) : base(context)
		{
		}
	}
}
