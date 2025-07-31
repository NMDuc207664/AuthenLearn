using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data.Seeding
{
    public static class ExpenseSeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model.Expense>().HasData(
                new Model.Expense
                {
                    Id = Guid.Parse("bc580c44-2332-4457-99c9-5ea4c096690a"),
                    Description = "tiền lương tháng 7",
                    Amount = 1000000,
                    Date = new DateTime(2025, 7, 1),
                    UserId = Guid.Parse("4090476F-2E7E-4884-9ECA-08DDC9CC4038")
                }
            );
        }
    }
}