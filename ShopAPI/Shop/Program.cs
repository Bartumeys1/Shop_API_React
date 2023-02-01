using DAL;
using DAL.Entities.Identity;
using DAL.Initializer;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Services;
using Services.Interfaces;
using Services.Services;
using Shop.Settings;

var builder = WebApplication.CreateBuilder(args);

//Add database context
builder.Services.AddDbContext<AppEFContext>(opt =>
{
    opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.

builder.Services.AddIdentity<UserEntity,RoleEntity>(opt =>
{
    opt.Password.RequireDigit=false;
    opt.Password.RequiredLength=5;
    opt.Password.RequireNonAlphanumeric=false;
    opt.Password.RequireUppercase=false;
    opt.Password.RequireLowercase=false;
}).AddEntityFrameworkStores<AppEFContext>()
.AddDefaultTokenProviders();

//Add section appjson injection
var googleAuthSettings = builder.Configuration
    .GetSection("GoogleAuthSettings")
    .Get<GoogleAuthSettings>();


builder.Services.AddSingleton(googleAuthSettings);

//Add controllers
builder.Services.AddControllers();
//Add Jwt service
builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();



//Add Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Add Cors
builder.Services.AddCors();

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

app.UseAuthorization();

string dir = Path.Combine(Directory.GetCurrentDirectory(),"images");
if (!Directory.Exists(dir))
{
    Directory.CreateDirectory(dir);
}
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider=new PhysicalFileProvider(dir),
    RequestPath="/images"
});

app.UseCors(options =>
            options.AllowAnyMethod()
            .AllowAnyOrigin()
            .AllowAnyHeader());

app.MapControllers();

app.SeedData();

app.Run();
