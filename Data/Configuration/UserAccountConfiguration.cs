using AuthenLearn.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthenLearn.Data.Configuration
{
    public class UserAccountConfiguration : IEntityTypeConfiguration<UserAccount>
    {
        public void Configure(EntityTypeBuilder<UserAccount> builder)
        {
            builder.HasKey(u => u.Id);
            builder.HasIndex(u => u.UserName)
                    .IsUnique();

            builder.HasIndex(u => u.Email)
                    .IsUnique();
            builder.HasOne(r => r.Role)
            .WithMany(u => u.UserAccount)
            .HasForeignKey(r => r.RoleId)
            .OnDelete(DeleteBehavior.Restrict);
        }
    }
}