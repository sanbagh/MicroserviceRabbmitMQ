using MicroserviceRabbitMQ.Domain.Core.Bus;
using MicroserviceRabbitMQ.Services.Banking.Application.Interfaces;
using MicroserviceRabbitMQ.Services.Banking.Application.Model;
using MicroserviceRabbitMQ.Services.Banking.Domain.Commands;
using MicroserviceRabbitMQ.Services.Banking.Domain.Interfaces;
using MicroserviceRabbitMQ.Services.Banking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Services.Banking.Application.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _repo;
        private readonly IEventBus _bus;

        public AccountService(IAccountRepository repo, IEventBus bus)
        {
            _repo = repo;
            _bus = bus;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _repo.GetAccounts();
        }

        public void Transfer(AccountTransfer accountTransfer)
        {
            var createTransferCommand = new CreateTransferCommand(
                    accountTransfer.FromAccount,
                    accountTransfer.ToAccount,
                    accountTransfer.TransferAmount
                );

            _bus.SendCommand(createTransferCommand);
        }
    }
}
