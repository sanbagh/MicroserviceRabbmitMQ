using MediatR;
using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Infra.Bus;
using MicroserviceRabbitMQ.Services.Banking.Application.Interfaces;
using MicroserviceRabbitMQ.Services.Banking.Application.Services;
using MicroserviceRabbitMQ.Services.Banking.Data.Context;
using MicroserviceRabbitMQ.Services.Banking.Data.Repository;
using MicroserviceRabbitMQ.Services.Banking.Domain.CommandHandler;
using MicroserviceRabbitMQ.Services.Banking.Domain.Commands;
using MicroserviceRabbitMQ.Services.Banking.Domain.Events;
using MicroserviceRabbitMQ.Services.Banking.Domain.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Application.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Application.Service;
using MicroserviceRabbitMQ.Services.Transfer.Data.Context;
using MicroserviceRabbitMQ.Services.Transfer.Data.EventHandlers;
using MicroserviceRabbitMQ.Services.Transfer.Data.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MicroserviceRabbitMQ.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            });
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<ITransferService, TransferService>();
            services.AddTransient<TransferEventHandler>();
            services.AddTransient<IEventHandler<TransferCreatedEvent>, TransferEventHandler>();
            services.AddTransient<IRequestHandler<CreateTransferCommand, bool>, TransferCommandHandler>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<ITransferRepository, TransferRepository>();
            services.AddTransient<BankingDbContext>();
            services.AddTransient<TransferDbContext>();
        }
    }
}
