namespace Hotel.DataAccess.Repositories.Implementations
{
	public class ReservationRepository : Repository<Reservation>, IReservationRepository
	{
		public ReservationRepository(AppDbContext context) : base(context)
		{
		}
	}
}
