
namespace Hotel.DataAccess.Repositories.Implementations
{
	public class SliderHomeRepository : Repository<SliderHome>, ISliderHomeRepository
	{
		public SliderHomeRepository(AppDbContext context) : base(context)
		{
		}
	}
}
