using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using WebDongTrung.Datas;
using WebDongTrung.Repositories;
using WebDongTrung.Repositories.CartClient;
using WebDongTrung.Repositories.Advertisment;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(option => option.AddDefaultPolicy(policy => policy.WithOrigins("http//localhost:3000","https//localhost:3000").AllowAnyHeader().AllowAnyMethod()));
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<StoreDbContex>().AddDefaultTokenProviders();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnect");
builder.Services.AddDbContext<StoreDbContex>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IProducts, ProductRepositories>();
builder.Services.AddScoped<IProductType, ProductTypeRepo>();
builder.Services.AddScoped<IEmployees, EmployeeRepo>();
builder.Services.AddScoped<ICart, CartRepo>();
builder.Services.AddScoped<ICartClient, CartClientRepo>();
builder.Services.AddScoped<IWarehouse, WarehouseRepo>();
builder.Services.AddScoped<IBlog, BlogRepo>();
builder.Services.AddScoped<ICartDetail, CartDetailRepo>();
builder.Services.AddScoped<IAdvertisment, AdvertisementRepo>();

builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.SaveToken = true;
    option.RequireHttpsMetadata = false;
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseStaticFiles();

app.Run();
