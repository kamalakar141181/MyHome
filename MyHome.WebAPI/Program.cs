using Microsoft.EntityFrameworkCore;
using MyHome.WebAPI.Business;
using MyHome.WebAPI.Context;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ITestProductBL, TestProductBL>();
// Microsoft.EntityFrameworkCore.SqlServer ( Tools -> Nuget Package Manager -> Manage Nuget Packages For Solutions -> Microsoft.EntityFrameworkCore.SqlServer search and install)
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
