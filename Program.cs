using System.Text.Json.Serialization;
using GenericContentParser.Middleware;
using GenericContentParser.Models;
using GenericContentParser.Services;
using GenericContentParser.Services.Parsers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(
            new JsonStringEnumConverter()
        );
    });



builder.Services.AddScoped<IContentParser,CsvParser>();
builder.Services.AddScoped<IContentParser,InternalJsonParser>();
builder.Services.AddScoped<ContentParserService>();


var app = builder.Build();



app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();