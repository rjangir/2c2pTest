using DemoSample.Domain.EF.Core.Abstractions;
using DemoSample.Domain.EF.Core.Entities;
using DemoSample.Domain.EF.Repositories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DemoSample.Domain.EF.Repositories.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddTransactions(IList<Transaction> transactions)
        {
            try
            {
                _context.Transactions.AddRange(transactions);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public IEnumerable<Transaction> GetTransactions(Func<Transaction,bool> predicate)
        {
            try
            {
                return _context.Transactions.Where(predicate);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
