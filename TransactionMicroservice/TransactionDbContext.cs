using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using TransactionMicroservice.Models;

namespace TransactionMicroservice.DataAccess
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity mappings and relationships
            modelBuilder.Entity<Transaction>()
                .HasKey(t => t.TransactionId);
        }
    }
}
