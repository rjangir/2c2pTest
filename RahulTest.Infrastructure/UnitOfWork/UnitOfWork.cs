using RahulTest.Core.Abstractions.UnitOfWork;
using RahulTest.Domain.EF.Core.Abstractions;
using RahulTest.Domain.EF.Repositories.Data;
using RahulTest.Domain.EF.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RahulTest.Infrastructure.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        public ITransactionRepository TransactionRepository => new TransactionRepository(_context);


        public Task<int> CommitAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
