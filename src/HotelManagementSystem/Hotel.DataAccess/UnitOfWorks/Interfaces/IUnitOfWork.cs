namespace Hotel.DataAccess.UnitOfWorks.Interfaces
{
	public interface IUnitOfWork
	{
		IAccountRepository accountRepository { get; }
		IAmentityRepository amentityRepository { get; }
		ICommentRepository commentRepository { get; }
		IFAQRepository fAQRepository { get; }
		IFlatRepository flatRepository { get; }
		IGallaryCatagoryRepository gallaryCatagoryRepository { get; }
		IGallaryImageRepository gallaryImageRepository { get; }
		INearPlaceRepository nearPlaceRepository { get; } 
		IReservationRepository reservationRepository { get; } 
		IReviewRepository reviewRepository { get; }
		IRoomCatagoryRepository roomCatagoryRepository { get; }
		IRoomImageRepository roomImageRepository { get;}
		ISelectedListRepository selectedListRepository { get; }
		ISentQuestionRepository sentQuestionRepository { get; }
		IServiceImageRepository serviceImageRepository { get; }
		IServiceOfferRepository serviceOfferRepository { get; } 
		ISettingTableRepository settingTableRepository { get; }
		ISliderHomeRepository sliderHomeRepository { get; }
		ITeamMemberRepository teamMemberRepository { get; } 
		ITeamMemberInfoRepository  teamMemberInfoRepository { get; }
		IUserInfoRepository userInfoRepository { get; }
		IWhyUsRepository whyUsRepository { get; }

		int Save();
		Task<int> SaveAsync();	


	}
}
