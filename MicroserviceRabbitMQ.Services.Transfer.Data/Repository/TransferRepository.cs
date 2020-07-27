using MicroserviceRabbitMQ.Services.Transfer.Data.Context;
using MicroserviceRabbitMQ.Services.Transfer.Data.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Services.Transfer.Data.Repository
{
    public class TransferRepository : ITransferRepository
    {
        private readonly TransferDbContext _context;

        public TransferRepository(TransferDbContext context)
        {
            _context = context;
        }
        public void Add(TransferLog transferLog)
        {
            _context.Add(transferLog);
            _context.SaveChanges();
        }

        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _context.TransferLogs;
        }
    }
}
