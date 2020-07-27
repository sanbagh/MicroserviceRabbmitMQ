using MicroserviceRabbitMQ.Services.Transfer.Application.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Data.Interfaces;
using MicroserviceRabbitMQ.Services.Transfer.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MicroserviceRabbitMQ.Services.Transfer.Application.Service
{
    public class TransferService : ITransferService
    {
        private readonly ITransferRepository _repo;

        public TransferService(ITransferRepository repo)
        {
           _repo = repo;
        }
        public IEnumerable<TransferLog> GetTransferLogs()
        {
            return _repo.GetTransferLogs();
        }
    }
}
