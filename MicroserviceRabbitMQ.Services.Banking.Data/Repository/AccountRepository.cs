using MicroserviceRabbitMQ.Services.Banking.Data.Context;
using MicroserviceRabbitMQ.Services.Banking.Domain.Interfaces;
using MicroserviceRabbitMQ.Services.Banking.Domain.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Services.Banking.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private BankingDbContext _ctx;

        public AccountRepository(BankingDbContext ctx)
        {
            _ctx = ctx;
        }
        public IEnumerable<Account> GetAccounts()
        {
            return _ctx.Accounts;
        }
    }
}
