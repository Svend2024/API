using Elasticsearch.Net;
using KameGameAPI.Database;
using KameGameAPI.Extenstions;
using KameGameAPI.Interfaces;
using KameGameAPI.Models;
using KameGameAPI.Repositories;
using KameGameAPI.Security;
using KameGameAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nest;
using System.Configuration;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddControllers();

var jwtSection = builder.Configuration.GetSection("JwtConfig");
builder.Services.Configure<JwtConfig>(jwtSection);

var appSettings = jwtSection.Get<JwtConfig>();
var key = Encoding.ASCII.GetBytes(appSettings.Secret);

builder.Services.AddElasticSearch(builder.Configuration);

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        ClockSkew = TimeSpan.Zero
    };
});

var models = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && typeof(BaseEntity).IsAssignableFrom(t));

foreach (var model in models)
{
    var baseServiceType = typeof(IBaseService<>).MakeGenericType(model);
    var baseRepositoryType = typeof(IBaseRepository<>).MakeGenericType(model);

    var baseServiceImplementationType = typeof(BaseService<>).MakeGenericType(model);
    var baseRepositoryImplementationType = typeof(BaseRepository<>).MakeGenericType(model);

    builder.Services.AddScoped(baseServiceType, baseServiceImplementationType);
    builder.Services.AddScoped(baseRepositoryType, baseRepositoryImplementationType);
}
builder.Services.AddScoped(typeof(ICustomerService), typeof(CustomerService));
builder.Services.AddScoped(typeof(ICustomerRepository), typeof(CustomerRepository));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
