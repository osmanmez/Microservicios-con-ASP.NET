using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventQueue>

    {
        private readonly ILogger<EmailEventoManejador> _logger;

        public EmailEventoManejador() {}

        public EmailEventoManejador(ILogger<EmailEventoManejador> logger)
        {
            _logger = logger;
        }

        public Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation($"Este es el valor que consume desde rabbitmq {@event.Titulo}");

                return Task.CompletedTask;
        }
    }
}
