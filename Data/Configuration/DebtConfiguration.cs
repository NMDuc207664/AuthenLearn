using AuthenLearn.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenLearn.Data.Configuration
{
    public class DebtConfiguration : IEntityTypeConfiguration<Debt>
    {
        public void Configure(EntityTypeBuilder<Debt> builder)
        {
            builder.HasKey(r => r.Id);

            builder.HasOne(r => r.UserAccount)
                .WithMany(u => u.Debts)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}