using RahulTest.Domain.EF.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RahulTest.Core.Abstractions.UnitOfWork
{
    public interface IUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }

        Task<int> CommitAsync();
    }
}
