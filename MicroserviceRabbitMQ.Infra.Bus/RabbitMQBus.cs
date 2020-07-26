using MediatR;
using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Domain.Core.Commands;
using MicroserviceRabbitMQ.Domain.Core.Events;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceRabbitMQ.Infra.Bus
{
    public class RabbitMQBus : IEventBus
    {
        private readonly IMediator _mediator;
        private readonly Dictionary<string, List<Type>> _eventHandlers;
        private readonly List<Type> _eventTypes;
        public RabbitMQBus(IMediator mediator)
        {
            _mediator = mediator;
            _eventHandlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }
        public void Publish<T>(T @event) where T : Event
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var name = @event.GetType().Name;
                channel.QueueDeclare(name, false, false, false, null);
                channel.BasicPublish("", name, null, Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event)));
            }
        }

        public Task SendCommand<T>(T command) where T : Command
        {
            return _mediator.Send(command);
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = typeof(T).Name;
            var eventType = typeof(T);
            var handlerType = typeof(TH);
            if (!_eventTypes.Contains(eventType))
            {
                _eventTypes.Add(eventType);
            }
            if (!_eventHandlers.ContainsKey(eventName))
            {
                _eventHandlers.Add(eventName, new List<Type>());
            }
            if (_eventHandlers[eventName].Any(x => x.GetType() == handlerType))
            {
                throw new ArgumentException($"Handler with this type {handlerType} is already registered for event name {eventName}");
            }
            _eventHandlers[eventName].Add(handlerType);
            StartBasicConsume<T>();
        }

        private void StartBasicConsume<T>() where T : Event
        {
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                DispatchConsumersAsync = true
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                var eventName = typeof(T).Name;
                channel.QueueDeclare(eventName, false, false, false, null);
                var consumer = new AsyncEventingBasicConsumer(channel);
                consumer.Received += Consumer_Received;
                channel.BasicConsume(eventName, true, consumer);
            }
        }

        private async Task Consumer_Received(object sender, BasicDeliverEventArgs @event)
        {
            try
            {
                await ProcessEvent(@event.RoutingKey, Encoding.UTF8.GetString(@event.Body.ToArray())).ConfigureAwait(false);
            }
            catch (Exception)
            {

            }
        }

        private async Task ProcessEvent(string eventName, string message)
        {
            if (_eventHandlers.ContainsKey(eventName))
            {
                var subscribers = _eventHandlers[eventName];
                if (subscribers != null && subscribers.Any())
                {
                    foreach (var subscriber in subscribers)
                    {
                        var handler = Activator.CreateInstance(subscriber);
                        if (handler == null) continue;
                        var eventType = _eventTypes.FirstOrDefault(x => x.Name == eventName);
                        var @event = JsonConvert.DeserializeObject(message, eventType);
                        var conreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        await (Task)conreteType.GetMethod("Handle").Invoke(handler, new object[] { @event });
                    }
                }
            }
        }
    }
}
