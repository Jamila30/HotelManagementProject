namespace Hotel.Business.Utilities.Extensions
{
	public static class ServiceExtensions
	{
		public static void AddAllServices(this WebApplicationBuilder builder)
		{
			#region Adding Repository injections
			builder.Services.AddScoped<ISliderHomeRepository, SliderHomeRepository>();
			builder.Services.AddScoped<IWhyUsRepository, WhyUsRepository>();
			builder.Services.AddScoped<INearPlaceRepository, NearPlaceRepository>();
			builder.Services.AddScoped<ITeamMemberRepository, TeamMemberRepository>();
			builder.Services.AddScoped<ITeamMemberInfoRepository, TeamMemberInfoRepository>();
			builder.Services.AddScoped<IServiceOfferRepository, ServiceOfferRepository>();
			builder.Services.AddScoped<IServiceImageRepository, ServiceImageRepository>();
			builder.Services.AddScoped<IGallaryImageRepository, GallaryImageRepository>();
			builder.Services.AddScoped<IGallaryCatagoryRepository, GallaryCatagoryRepository>();
			builder.Services.AddScoped<IFlatRepository, FlatRepository>();
			builder.Services.AddScoped<IRoomImageRepository, RoomImageRepository>();
			builder.Services.AddScoped<IRoomCatagoryRepository, RoomCatagoryRepository>();
			builder.Services.AddScoped<ICommentRepository, CommentRepository>();
			builder.Services.AddScoped<IAmentityRepository, AmentityRepository>();
			builder.Services.AddScoped<IAccountRepository, AccountRepository>();
			builder.Services.AddScoped<ISentQuestionRepository, SentQuestionRepository>();
			builder.Services.AddScoped<IFAQRepository, FAQRepository>();
			builder.Services.AddScoped<IUserInfoRepository, UserInfoRepository>();
			builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
			builder.Services.AddScoped<ISelectedListRepository, SelectedListRepository>();
			builder.Services.AddScoped<ISettingTableRepository, SettingsTableRepository>();
			#endregion

			#region Adding Service Injections
			builder.Services.AddScoped<ISliderHomeService, SliderHomeService>();
			builder.Services.AddScoped<IWhyUsService, WhyUsService>();
			builder.Services.AddScoped<INearPlaceService, NearPlaceService>();
			builder.Services.AddScoped<ITeamMemberInfoService, TeamMemberInformationService>();
			builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
			builder.Services.AddScoped<IServiceOfferService, ServiceOfferService>();
			builder.Services.AddScoped<IServiceImageService, ServiceImageService>();
			builder.Services.AddScoped<IGallaryCatagoryService, GallaryCatagoryService>();
			builder.Services.AddScoped<IGallaryImageService, GallaryImageService>();
			builder.Services.AddScoped<IFlatService, FlatService>();
			builder.Services.AddScoped<IRoomImageService, RoomImageService>();
			builder.Services.AddScoped<IRoomCatagoryService, RoomCatagoryService>();
			builder.Services.AddScoped<ICommentService, CommentService>();
			builder.Services.AddScoped<IAmentityService, AmentityService>();
			builder.Services.AddScoped<IAutService, AuthService>();
			builder.Services.AddScoped<ITokenCreatorService, TokenCreatorService>();
			builder.Services.AddScoped<IAccountService, Hotel.Business.Services.Implementations.AccountService>();
			builder.Services.AddScoped<ISentQuestionService, SentQuestionService>();
			builder.Services.AddScoped<IFaqService, FaqService>();
			builder.Services.AddScoped<IUserInfoService, UserInfoService>();
			builder.Services.AddScoped<IReservationService, ReservationService>();
			builder.Services.AddScoped<ISelectedListService, SelectedListService>();
			builder.Services.AddScoped<ISettingsService, SettingsService>();
			#endregion

			#region Adding Stripe Injections
			builder.Services.AddScoped<TokenService>();
			builder.Services.AddScoped<CustomerService>();
			builder.Services.AddScoped<ChargeService>();
			builder.Services.AddScoped<IStripeService, StripeService>();
			StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:SecretKey");//4242424242424242
			#endregion

			#region UnitOFWork Injections
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			#endregion

			#region Adding Fluent Validations
			builder.Services.AddFluentValidationAutoValidation();
			builder.Services.AddFluentValidationClientsideAdapters();
			builder.Services.AddValidatorsFromAssemblyContaining<SliderHomeValidator>();
			#endregion

		}
	}
}
