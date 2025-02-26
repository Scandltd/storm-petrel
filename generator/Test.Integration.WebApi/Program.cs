var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

var app = builder.Build();
app.MapControllers();
app.Run();

public partial class Program { } //Allow to find this class from other projects