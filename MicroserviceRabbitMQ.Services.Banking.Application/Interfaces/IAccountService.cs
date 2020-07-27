using MicroserviceRabbitMQ.Services.Banking.Application.Model;
using MicroserviceRabbitMQ.Services.Banking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Services.Banking.Application.Interfaces
{
    public interface IAccountService
    {
        IEnumerable<Account> GetAccounts();
        void Transfer(AccountTransfer accountTransfer);
    }
}
