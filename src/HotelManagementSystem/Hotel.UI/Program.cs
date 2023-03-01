using Hotel.Business.Utilities.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Connecting DBContext
var conString = builder.Configuration["ConnectionStrings:default"];
builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conString));
builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
{
	
	opt.Lockout.AllowedForNewUsers = false;
	opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
	opt.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

//Adding CORS
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
					  policy =>
					  {
						  policy.WithOrigins("http://localhost:3000").AllowAnyMethod().AllowAnyHeader();
					  });
});

//Adding AppDbContextInitializer
IServiceCollection serviceCollection = builder.Services.AddScoped<AppDbContextInitializer>();

//MailSender Configuration
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
builder.Services.AddTransient<IMailService, MailService>();

//Adding Repository and Services injections
builder.AddAllServices();

//Adding Mapper configuration
builder.Services.AddAutoMapper(typeof(SliderHomeMapper).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Adding JWT Configurations
builder.Services.AddAuthentication(option =>
{
	option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opt =>
{
	opt.SaveToken = true;
	opt.TokenValidationParameters = new TokenValidationParameters()
	{
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecurityKey"])),
		LifetimeValidator = (_, expires, _, _) => expires != null ? expires > DateTime.UtcNow : true,

		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
		ClockSkew = TimeSpan.Zero,
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

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);
// [EnableCors("MyAllowSpecificOrigins")]
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
