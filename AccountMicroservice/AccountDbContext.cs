using Microsoft.EntityFrameworkCore;
using AccountMicroservice.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AccountMicroservice
{
    public class AccountDbContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } 

        public AccountDbContext(DbContextOptions<AccountDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().ToTable("Account");
            // Configure entity mappings and relationships
            modelBuilder.Entity<Account>()
                .HasKey(a => a.AccountNumber);
        }
    }
}
