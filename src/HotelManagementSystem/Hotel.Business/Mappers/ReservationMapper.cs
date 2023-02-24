using Hotel.Business.DTOs.ReservationDTOs;

namespace Hotel.Business.Mappers
{
	public class ReservationMapper:Profile
	{
		public ReservationMapper()
		{
			CreateMap<Reservation,ReservationDto>().ReverseMap();
			CreateMap<Reservation,UpdateReservationDto>().ReverseMap();
		}
	}
}
