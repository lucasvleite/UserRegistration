using System.Text;
using UserRegistration.WebApi.Data;
using UserRegistration.WebApi.EndPoints;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<UserRegistrationContext>();
builder.Services.AddDistributedMemoryCache();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var jwtKey = Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]);
//builder.Services.AddJwtConfigurations(jwtKey);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapUserEndpoints();
app.MapLoginEndpoints(jwtKey);

DbInitialize.InitializeUsers();

app.Run();
