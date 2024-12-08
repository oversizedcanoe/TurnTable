using TurnTableAPI.ActionFilters;
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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowLocalhost",
        policy =>
        {
            policy.WithOrigins("https://localhost:7282", "http://localhost:4200", "http://192.168.2.34:4200", "https://192.168.2.34:7282")
            .AllowAnyHeader().AllowAnyMethod().AllowCredentials();
        });
});

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

app.UseCors("AllowLocalhost");

app.MapHub<GameHub>("/gamehub");

app.Run();
