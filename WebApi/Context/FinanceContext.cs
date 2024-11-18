using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Context;

public class FinanceContext : DbContext
{
    public FinanceContext(DbContextOptions<FinanceContext> options) : base(options) { }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<Operation> Operations { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        base.OnModelCreating(modelBuilder);
    }
}