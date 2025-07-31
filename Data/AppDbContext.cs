using AuthenLearn.Data.Configuration;
using AuthenLearn.Data.Seeding;
using AuthenLearn.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<UserAccount> UserAccounts { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<Debt> Debts { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserAccountConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ExpenseConfiguration());
            modelBuilder.ApplyConfiguration(new DebtConfiguration());
            AccountSeedData.Seed(modelBuilder);
            RoleSeedData.Seed(modelBuilder);
            ExpenseSeedData.Seed(modelBuilder);
        }
    }
}