using Microsoft.EntityFrameworkCore;
using RahulTest.Domain.EF.Core.Entities;
using System.Text;

namespace RahulTest.Domain.EF.Repositories.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>().Property(x => x.TransactionIdentifier).HasMaxLength(50);
            modelBuilder.Entity<Transaction>().Property(x => x.Amount).HasPrecision(12, 10);
        }
    }
}
