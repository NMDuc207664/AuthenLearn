using AuthenLearn.Data.Common;
using AuthenLearn.Data.Common.QueryParameters;
using AuthenLearn.Model;
using AuthenLearn.Model.Enum;

namespace AuthenLearn.Applications.Interfaces
{
    public interface IExpenseRepository : IGenericRepository<Expense>
    {
        Task<PagedResult<Expense>> GetByUserIdAsync(Guid userId, ExpenseQueryParameter query);
        Task<List<Expense>> GetExpensesByDebtIdAsync(Guid debtId, ExpenseType expenseType, Guid? excludeExpenseId = null);
        Task<List<Expense>> GetAllExpensesByYearAsync(Guid userId, int year);
        Task<List<Expense>> GetAllExpensesByMonthAsync(Guid userId, int year, int month);
    }
}