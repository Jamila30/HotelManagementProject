using Hotel.Business.DTOs.SentQuestionDTOs;

namespace Hotel.Business.Mappers
{
	public class SentQuestionMapper:Profile
	{
		public SentQuestionMapper()
		{
			CreateMap<SentQuestion,SentQuestionDto>().ReverseMap();

		}
	}
}
