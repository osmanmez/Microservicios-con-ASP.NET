using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TiendaServicios.Api.Autor.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ContextoAutor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase"));
}
    );

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
