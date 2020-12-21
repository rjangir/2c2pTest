using DemoSample.Core.Abstractions.Services;
using DemoSample.Core.Abstractions.UnitOfWork;
using DemoSample.Core.Dtos;
using DemoSample.Domain.EF.Core.Abstractions;
using DemoSample.Domain.EF.Core.Entities;
using DemoSample.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoSample.Infrastructure.Services
{

    public class TransactionsService : ITransactionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransactionsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task SaveTransactions(IList<Transaction> transactions)
        {
            _unitOfWork.TransactionRepository.AddTransactions(transactions);
            await _unitOfWork.CommitAsync();
        }

        public TransactionDto GetById(string id)
        {
            var transactions = _unitOfWork.TransactionRepository.GetTransactions(x => x.TransactionIdentifier == id).FirstOrDefault();
            return transactions.ToTransactionDto();
        }
        public IEnumerable<TransactionDto> GetByCurrency(string currencyCode)
        {
            var transactions = _unitOfWork.TransactionRepository.GetTransactions(x => x.CurrencyCode== currencyCode);
            return transactions.ToTransactionDtos();
        }
        public IEnumerable<TransactionDto> GetByDateRange(DateTime dateTime)
        {
            var transactions = _unitOfWork.TransactionRepository.GetTransactions(x => x.TransactionDate == dateTime);
            return transactions.ToTransactionDtos();
        }
        public IEnumerable<TransactionDto> GetByStatus(TransactionStatus status)
        {
            var transactions = _unitOfWork.TransactionRepository.GetTransactions(x => x.Status == status);
            return transactions.ToTransactionDtos();
        }
    }
}
