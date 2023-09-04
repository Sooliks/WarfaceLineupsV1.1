using Microsoft.Net.Http.Headers;
using WarfaceLineupsV1._1.Database;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "_myAllowSpecificOrigins",
        policy =>
        {
            policy.AllowAnyHeader().AllowAnyMethod();
            policy.WithOrigins("http://localhost:3000");
            policy.WithHeaders(HeaderNames.ContentType);
        });
});
var app = builder.Build();

app.UseCors("_myAllowSpecificOrigins");
app.MapControllers();
app.MapGet("/", () => "Hello World!");


using (Context db = new Context())
{
    try
    {
        Console.WriteLine(db.Database.CanConnect() ? "Database success connected!" : "Database is unavailable!");
    }
    catch (Exception e)
    {
        Console.WriteLine(e.ToString());
    }
}

app.Run();