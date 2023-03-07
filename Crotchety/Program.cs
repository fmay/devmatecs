using Crotchety.Services;
using Crotchety.Repository.Db;
using Crotchety.Core;
using Microsoft.Extensions.Hosting;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Logging
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add global exception handler
builder.Services.AddTransient<GlobalExceptionHandler>();

var app = builder.Build();

// Config Service
IConfiguration config = app.Services.GetRequiredService<IConfiguration>();
ConfigService configService = ConfigService.Instance;
configService.LoadConfig(config);

// Database Service
DatabaseService n4j = DatabaseService.Instance;
n4j.Init(config);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandler>();

app.MapControllers();

app.Run();
