using Hotel.DataAccess.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Connecting DBContext
var conString = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conString));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//Adding Fluent Validations
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<SliderHomeValidator>();

//Adding AppDbContextInitializer
builder.Services.AddScoped<AppDbContextInitializer>();

//MailSender Configuration
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

//Adding Repository injections
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



//Adding Service Injections
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
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<ITokenCreatorService,TokenCreatorService>();
builder.Services.AddScoped<IAccountService, AccountService>();


//Adding Mapper configuration
builder.Services.AddAutoMapper(typeof(SliderHomeMapper).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding JWT Configuration
builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
	opt.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"])),
		LifetimeValidator = (_, expires, _, _) => expires != null ? expires > DateTime.UtcNow : true,

		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true
	};
});
builder.Services.AddAuthorization();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

//AppDBContextInitializer scope using 
using (var scope = app.Services.CreateScope())
{
	var initializer = scope.ServiceProvider.GetRequiredService<AppDbContextInitializer>();
	await initializer.InitializeAsync();
	await initializer.RoleSeedAsync();
	await initializer.UserSeedAsync();
}


app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
