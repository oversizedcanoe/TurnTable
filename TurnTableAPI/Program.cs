using TurnTableAPI.ActionFilters;
using TurnTableDomain.Hubs;
using TurnTableDomain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<GameManager>();
builder.Services.AddScoped<ValidGameCodeFilter>();
builder.Services.AddScoped<GameHub>();

builder.Services.AddSignalR();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();
builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowLocalhost",
        policy =>
        {
            policy.WithOrigins(
                "http://localhost:4200", "http://localhost:7282",
                "https://localhost:4200", "https://localhost:7282",
                "http://192.168.2.34:4200", "http://192.168.2.34:7282",
                "https://192.168.2.34:4200", "https://192.168.2.34:7282")
            .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.EnableTryItOutByDefault();
    });
}

app.UseCors("AllowLocalhost");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.MapHub<GameHub>("/gamehub");

app.Run();
