using AuthenLearn.Model;
using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data.Seeding
{
    public static class AccountSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserAccount>().HasData(
                new UserAccount
                {
                    Id = Guid.Parse("bc880c44-2632-4457-99c9-5ea4c096690a"),
                    UserName = "Admin",
                    Password = "Admin123",
                    RoleId = 1
                },
                new UserAccount
                {
                    Id = Guid.Parse("84057361-30b5-4840-a71b-28d951cc0653"),
                    UserName = "User",
                    Password = "User123",
                    RoleId = 2
                }
            );
        }
    }
}