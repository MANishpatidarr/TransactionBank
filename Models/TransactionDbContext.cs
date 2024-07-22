using Microsoft.EntityFrameworkCore;

namespace TransactionBank.Models
{
    public class BankTransactionDbContext:DbContext
    {


        public BankTransactionDbContext(DbContextOptions<BankTransactionDbContext> options) : base(options) 
        { }

        public DbSet<Transaction> Transaction { get; set; }  
    }
}
