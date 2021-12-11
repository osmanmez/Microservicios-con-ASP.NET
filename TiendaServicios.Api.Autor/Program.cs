using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TiendaServicios.Api.Autor.Aplicacion;
using TiendaServicios.Api.Autor.Persistencia;
using MediatR;
using FluentValidation.AspNetCore;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;
using TiendaServicios.Api.Autor.ManejadorRabbit;
using TiendaServicios.RabbitMQ.Bus.Implement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Nuevo>());


builder.Services.AddDbContext<ContextoAutor>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConexionDatabase"));
}
    );

builder.Services.AddMediatR(typeof(Nuevo.Manejador).Assembly);
builder.Services.AddSingleton<IRabbitEventBus, RabbitEventBus>(sp =>
{
    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
    return new RabbitEventBus(sp.GetService<IMediator>(), scopeFactory);
});

builder.Services.AddTransient<EmailEventoManejador>();

builder.Services.AddTransient<IEventoManejador<EmailEventQueue>, EmailEventoManejador>();

builder.Services.AddAutoMapper(typeof(Consulta.Manejador));

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();


app.MapControllers();


var eventBus = app.Services.GetRequiredService<IRabbitEventBus>();
eventBus.Subscribe<EmailEventQueue, EmailEventoManejador>();

app.Run();
