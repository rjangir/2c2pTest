using RahulTest.Core.Abstractions.Services;
using RahulTest.Core.Abstractions.UnitOfWork;
using RahulTest.Core.Dtos;
using RahulTest.Domain.EF.Core.Abstractions;
using RahulTest.Domain.EF.Core.Entities;
using RahulTest.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahulTest.Infrastructure.Services
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

        public IEnumerable<TransactionDto> GetById(string id)
        {
            var transactions = _unitOfWork.TransactionRepository.GetTransactions(x => x.TransactionIdentifier == id);
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
