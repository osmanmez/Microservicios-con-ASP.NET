using FluentValidation;
using MediatR;
using TiendaServicios.Api.Libros.Modelo;
using TiendaServicios.Api.Libros.Persistencia;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Libro.Aplicacion
{
    public class Nuevo
    {

        public class Ejecuta : IRequest
        {

            public string Titulo { get; set; }
            public string? FechaPublicacion { get; set; }
            public Guid? AutorLibro { get; set; }


            public class EjecutaValidacion : AbstractValidator<Ejecuta>
            {
                public EjecutaValidacion()
                {
                    RuleFor(x => x.Titulo).NotEmpty();
                    RuleFor(x => x.FechaPublicacion).NotEmpty();
                    RuleFor(x => x.AutorLibro).NotEmpty();
                }

            }
        }


        public class Manejador : IRequestHandler<Ejecuta>
        {

            public readonly ContextoLibreria _contexto;
            private readonly IRabbitEventBus _eventBus;


            public Manejador(ContextoLibreria contexto, IRabbitEventBus eventBus)
            {
                _contexto = contexto;
                _eventBus = eventBus;
            }


            public async Task<Unit> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                var libro = new LibreriaMaterial
                {
                    Titulo = request.Titulo,
                    FechaPublicacion = request.FechaPublicacion,
                    AutorLibro = request.AutorLibro
                };

                _contexto.LibreriaMaterial.Add(libro);
                var value = await _contexto.SaveChangesAsync();

                _eventBus.Publish(new EmailEventQueue("osman.mazariegos@gmail.com", request.Titulo, "Este email es un ejemplo."));

                if (value > 0)
                {
                    return Unit.Value;
                }


                throw new Exception("No se pudo guardar el libro");


            }


        }

    }
}
