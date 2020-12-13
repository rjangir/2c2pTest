using RahulTest.Domain.EF.Core.Entities;
using System;
using System.Collections.Generic;

namespace RahulTest.Domain.EF.Core.Abstractions
{
    public interface ITransactionRepository
    {
        void AddTransactions(IList<Transaction> transactions);
        IEnumerable<Transaction> GetTransactions(Func<Transaction, bool> predicate);
    }
}