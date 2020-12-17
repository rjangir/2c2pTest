using DemoSample.Core.Dtos;
using DemoSample.Domain.EF.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DemoSample.Core.Abstractions.Services
{
    public interface ITransactionsService
    {
        IEnumerable<TransactionDto> GetByDateRange(DateTime dateTime);
        IEnumerable<TransactionDto> GetById(string id);
        IEnumerable<TransactionDto> GetByStatus(TransactionStatus status);
        Task SaveTransactions(IList<Transaction> transactions);
    }
}