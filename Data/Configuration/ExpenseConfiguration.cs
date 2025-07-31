using AuthenLearn.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenLearn.Data.Configuration
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.UserAccount)
                .WithMany(u => u.Expenses)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.Debt)
                .WithMany(d => d.Expenses)
                .HasForeignKey(e => e.DebtId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}