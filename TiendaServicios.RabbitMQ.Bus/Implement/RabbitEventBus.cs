using MediatR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using TiendaServicios.RabbitMQ.Bus.BusRabbit;
using TiendaServicios.RabbitMQ.Bus.Comandos;
using TiendaServicios.RabbitMQ.Bus.Eventos;

namespace TiendaServicios.RabbitMQ.Bus.Implement
{
    public class RabbitEventBus : IRabbitEventBus
    {

        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _manejadores;
        private readonly List<Type> _eventoTipos;

        public RabbitEventBus(IMediator mediator)
        {
            _mediator = mediator;
            _manejadores = new Dictionary<string, List<Type>>();
            _eventoTipos = new List<Type>();
        }

        public Task EnviarComando<T>(T comando) where T : Comando
        {
           return _mediator.Send(comando);
        }

        public void Publish<T>(T evento) where T : Evento
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost"
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = evento.GetType().Name;

                channel.QueueDeclare(eventName, false, false, false, null);

                var mesage = JsonConvert.SerializeObject(evento);
                var body = Encoding.UTF8.GetBytes(mesage);
                channel.BasicPublish("", eventName, null, body);

            }

        }

        public void Subscribe<T, TH>()
            where T : Evento
            where TH : IEventoManejador<T>
        {
            throw new NotImplementedException();
        }
    }
}
