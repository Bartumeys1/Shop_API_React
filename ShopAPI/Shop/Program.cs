using DAL;
using DAL.Entities.Identity;
using DAL.Initializer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllers();
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

app.MapControllers();

app.SeedData();

app.Run();
