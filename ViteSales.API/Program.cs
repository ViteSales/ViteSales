using System.Text.Json.Serialization;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using ViteSales.API.Binders;
using ViteSales.API.Middleware;
using ViteSales.API.Utils;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File(new JsonFormatter(), "logs/critical.json", restrictedToMinimumLevel: LogEventLevel.Error)
    .WriteTo.File(new JsonFormatter(), "logs/warning.json", restrictedToMinimumLevel: LogEventLevel.Warning)
    .WriteTo.File("logs/vitesales-.logs", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Debug()
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSerilog();
builder.Services.AddOpenApi();
builder.Services.AddControllers(options =>
{
    options.ModelBinderProviders.Insert(0, new JsonHeaderModelBinderProvider());
});
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    options.JsonSerializerOptions.WriteIndented = true;
    options.JsonSerializerOptions.Converters.Add(new JsonDateTimeConverter());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.UseMiddleware<ExceptionHandlerMiddleware>();

await app.RunAsync();