using DemoSample.Core.Abstractions.UnitOfWork;
using DemoSample.Domain.EF.Core.Abstractions;
using DemoSample.Domain.EF.Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DemoSample.Domain.EF.Repositories.Data;

namespace DemoSample.Infrastructure.UnitOfWork
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
