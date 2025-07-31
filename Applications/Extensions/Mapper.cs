using AuthenLearn.Applications.DTOs.Response;
using AuthenLearn.Model;

namespace AuthenLearn.Applications.Extensions
{
    public static class Mapper
    {
        public static ExpenseResponse ToExpenseResponse(this Expense expense, string title)
        {
            return new ExpenseResponse
            {
                Description = expense.Description,
                Amount = expense.Amount,
                Date = expense.Date,
                Title = title
            };
        }
        public static MoneyResponse ToMoneyResponse(this double amount)
        {
            return new MoneyResponse
            {
                TotalAmount = amount
            };
        }
    }
}