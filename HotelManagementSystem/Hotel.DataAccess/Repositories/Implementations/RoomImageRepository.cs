namespace Hotel.DataAccess.Repositories.Implementations
{
	public class RoomImageRepository : Repository<RoomImage>, IRoomImageRepository
	{
		public RoomImageRepository(AppDbContext context) : base(context)
		{

		}
	}
}
