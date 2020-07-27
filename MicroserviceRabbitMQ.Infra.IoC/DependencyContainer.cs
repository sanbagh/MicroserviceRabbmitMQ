using MediatR;
using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Infra.Bus;
using MicroserviceRabbitMQ.Services.Banking.Application.Interfaces;
using MicroserviceRabbitMQ.Services.Banking.Application.Services;
using MicroserviceRabbitMQ.Services.Banking.Data.Repository;
using MicroserviceRabbitMQ.Services.Banking.Domain.CommandHandler;
using MicroserviceRabbitMQ.Services.Banking.Domain.Commands;
using MicroserviceRabbitMQ.Services.Banking.Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceRabbitMQ.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IEventBus, RabbitMQBus>();
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
            services.AddTransient<IAccountRepository, AccountRepository>();
        }
    }
}
