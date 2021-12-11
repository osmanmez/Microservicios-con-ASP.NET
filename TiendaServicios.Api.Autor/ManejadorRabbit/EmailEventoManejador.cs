using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.EventoQueue;

namespace TiendaServicios.Api.Autor.ManejadorRabbit
{
    public class EmailEventoManejador : IEventoManejador<EmailEventQueue>

    {
        private readonly ILogger<EmailEventoManejador> _logger;


        public EmailEventoManejador(ILogger<EmailEventoManejador> logger)
        {
            _logger = logger;
        }


        public EmailEventoManejador() {}

        public Task Handle(EmailEventQueue @event)
        {
            _logger.LogInformation(@event.Titulo);

                return Task.CompletedTask;
        }
    }
}
