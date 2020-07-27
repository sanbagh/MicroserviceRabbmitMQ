using MediatR;
using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Services.Banking.Domain.Commands;
using MicroserviceRabbitMQ.Services.Banking.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceRabbitMQ.Services.Banking.Domain.CommandHandler
{
    public class TransferCommandHandler : IRequestHandler<CreateTransferCommand,bool>
    {
        private readonly IEventBus _bus;

        public TransferCommandHandler(IEventBus bus)
        {
            _bus = bus;
        }
        public Task<bool> Handle(CreateTransferCommand request, CancellationToken cancellationToken)
        {
            _bus.Publish(new TransferCreatedEvent(request.From, request.To, request.Amount));

            return Task.FromResult(true);
        }
    }
}
