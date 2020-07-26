using MicroserviceRabbitMQ.Domain.Core.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceRabbitMQ.Domain.Core.Bus
{
    public interface IEventHandler<in T> :IEventHandler where T: Event
    {
        Task Handle(T @event);
    }
    public interface IEventHandler
    {

    }
}
