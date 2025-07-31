using AuthenLearn.Applications.DTOs.Response;
using AuthenLearn.Data.Common;
using AuthenLearn.Data.Common.QueryParameters;
using AuthenLearn.Model;

namespace AuthenLearn.Applications.Interfaces.ServicesInterfaces
{
    public interface IExpenseService
    {
        Task<PagedResult<Expense>> GetBookByPagination(Guid userId, ExpenseQueryParameter query);
        void AddExpense(Expense request, Guid userId);
        Task UpdateExpense(Expense request);
        Task DeleteExpense(Guid expenseId);
        Task<MoneyResponse> GetAllExpensesPayByYear(Guid userId, int year);
        Task<MoneyResponse> GetAllExpensesPayByMonth(Guid userId, int year, int month);
        Task<MoneyResponse> GetAllRemainingDebtByMonth(Guid userId, int year, int month);
        Task<MoneyResponse> GetAllRemainingDebtByYear(Guid userId, int year);
        Task<MoneyResponse> GetAllExpensesByCondition(Guid userId, string key, DateTime? time);
    }
}