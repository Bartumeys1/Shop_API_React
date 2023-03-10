using DAL;
using DAL.Entities.Identity;
using DAL.Initializer;
using DAL.Interfaces;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Services.AutoMapper;
using Services.Interfaces;
using Services.Services;
using Shop.Settings;
using System.Text;

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
builder.Services.AddScoped<IProductService, ProductServices>();


//Add Repositories
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();

//Add AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProductEntityProduct));
builder.Services.AddAutoMapper(typeof(AutoMapperCategoryEntityCategoryEntity));

//Add Cors
builder.Services.AddCors();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

SymmetricSecurityKey signinKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JwtKey")));
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters()
    {
        IssuerSigningKey = signinKey,
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseAuthentication();
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
