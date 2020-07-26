using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceRabbitMQ.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMQBus>();
        }
    }
}
