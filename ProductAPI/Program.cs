using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//builder.Services.AddDbContext<DatabaseContext>(
//			options => options.UseNpgsql(builder.Configuration["database"]));
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);


builder.Services.AddAuthentication(x =>
{
	x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
	x.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuer = true,
		ValidateAudience = true,
		ValidateIssuerSigningKey = true,
		ValidateLifetime = true,
		ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
		ValidAudience = builder.Configuration["JwtSettings:Audience"],
		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]!))
	};

});

builder.Services.AddAuthorization(options =>
{
	var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(
		JwtBearerDefaults.AuthenticationScheme);

	defaultAuthorizationPolicyBuilder =
		defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();

	options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
});

builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bitirme.API", Version = "v1" });

	c.AddSecurityDefinition(
		"Bearer",
		new OpenApiSecurityScheme
		{
			Name = "Authorization",
			In = ParameterLocation.Header,
			Type = SecuritySchemeType.ApiKey,
			Scheme = "Bearer"
		}
	);
	c.AddSecurityRequirement(
		new OpenApiSecurityRequirement()
		{
			{
				new OpenApiSecurityScheme
				{
					Reference = new OpenApiReference
					{
						Type = ReferenceType.SecurityScheme,
						Id = "Bearer"
					},
					Scheme = "oauth2",
					Name = "Bearer",
					In = ParameterLocation.Header,
				},
				new List<string>()
			}
		}
	);

	c.CustomSchemaIds(type => type.ToString());
});

builder.Services.AddCors(c =>
{
	c.AddPolicy("All", p => p.AllowAnyHeader().AllowAnyOrigin().AllowAnyMethod());
});


builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddScoped<IUserDal, EfUserDal>();
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped(typeof(SecurityService));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();

}
app.UseCors("All");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
