using Microsoft.EntityFrameworkCore;
using MyHome.WebAPI.Business;
using MyHome.WebAPI.Context;
using MyHome.WebAPI.Helpers;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<ITestProductBL, TestProductBL>();
builder.Services.AddDbContext<AppDBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection")));
builder.Services.AddTransient<ITestProductBLUsingADODotNet, TestProductBLUsingADODotNet>();
builder.Services.AddTransient<ISqlHelper, SqlHelper>();
builder.Services.AddTransient<IConnection, Connection>();
builder.Services.AddTransient<IUtilityBL, UtilityBL>();
builder.Services.AddSwaggerGen();
builder.Logging.AddNLog();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
