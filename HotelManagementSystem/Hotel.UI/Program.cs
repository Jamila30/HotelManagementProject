

using Hotel.Business.Services.Implementations;
using Hotel.Business.Services.Interfaces;
using Hotel.DataAccess.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//Connecting DBContext
var conString = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conString));

//Adding Fluent Validations
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<SliderHomeValidator>();

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



//Adding Service Injections
builder.Services.AddScoped<ISliderHomeService, SliderHomeService>();
builder.Services.AddScoped<IWhyUsService, WhyUsService>();
builder.Services.AddScoped<INearPlaceService,NearPlaceService>();
builder.Services.AddScoped<ITeamMemberInfoService,TeamMemberInformationService>();
builder.Services.AddScoped<ITeamMemberService , TeamMemberService>();
builder.Services.AddScoped<IServiceOfferService , ServiceOfferService>();
builder.Services.AddScoped<IServiceImageService , ServiceImageService>();
builder.Services.AddScoped<IGallaryCatagoryService , GallaryCatagoryService>();
builder.Services.AddScoped<IGallaryImageService, GallaryImageService>();
builder.Services.AddScoped<IFlatService,FlatService>();
builder.Services.AddScoped<IRoomImageService,RoomImageService>();
builder.Services.AddScoped<IRoomCatagoryService,RoomCatagoryService>();
builder.Services.AddScoped<ICommentService,CommentService>();

//Adding Mapper configuration
builder.Services.AddAutoMapper(typeof(SliderHomeMapper).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
