using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Services.Banking.Domain.Events;
using MicroserviceRabbitMQ.Services.Transfer.Data.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceRabbitMQ.Services.Transfer.Data.EventHandlers
{
    public class TransferEventHandler : IEventHandler<TransferCreatedEvent>
    {
        private readonly ITransferRepository _transferRepository;
        public TransferEventHandler() { }
        public TransferEventHandler(ITransferRepository transferRepository)
        {
            _transferRepository = transferRepository;
        }
        public Task Handle(TransferCreatedEvent @event)
        {
            var log = new TransferLog
            {
                FromAccount = @event.From,
                ToAccount = @event.To,
                TransferAmount = @event.Amount
            };
            _transferRepository.Add(log);
            return Task.CompletedTask;
        }
    }
}
