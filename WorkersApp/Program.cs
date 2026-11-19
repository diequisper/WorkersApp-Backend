using WorkersApp;
using WorkersApp.Repository;
using WorkersApp.Services;

var builder = WebApplication.CreateBuilder(args);

//db config
DotNetEnv.Env.Load();
builder.Configuration.AddEnvironmentVariables();

builder.Services.AddSingleton<DbConnection>();

// Add services to the container.
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<UsuarioRepository>();

builder.Services.AddScoped<TrabajadorService>();
builder.Services.AddScoped<TrabajadorRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
