using MicroserviceRabbitMQ.Services.Banking.Domain.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Services.Banking.Data.Context
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
    }
}
