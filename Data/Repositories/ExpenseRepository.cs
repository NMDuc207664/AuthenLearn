using System.Linq.Expressions;
using AuthenLearn.Applications.Interfaces;
using AuthenLearn.Data.Common;
using AuthenLearn.Data.Common.QueryParameters;
using AuthenLearn.Model;
using AuthenLearn.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data.Repositories
{
    public class ExpenseRepository : GenericRepository<Expense>, IExpenseRepository
    {
        private readonly AppDbContext _context;
        public ExpenseRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Expense>> GetAllExpensesByMonthAsync(Guid userId, int year, int month)
        {
            return await _context.Expenses
       .Where(e => e.UserId == userId && e.Date.Year == year && e.Date.Month == month)
       .ToListAsync();
        }

        public async Task<List<Expense>> GetAllExpensesByYearAsync(Guid userId, int year)
        {
            return await _context.Expenses
        .Where(e => e.UserId == userId && e.Date.Year == year)
        .ToListAsync();
        }

        public async Task<PagedResult<Expense>> GetByUserIdAsync(Guid userId, ExpenseQueryParameter query)
        {
            Expression<Func<Expense, bool>> filter = e =>
        e.UserId == userId &&
        (!query.Year.HasValue || e.Date.Year == query.Year.Value) &&
        (!query.Month.HasValue || e.Date.Month == query.Month.Value) &&
        (!query.Day.HasValue || e.Date.Day == query.Day.Value) &&
         (!query.Paid.HasValue ||
         (query.Paid.Value ? (e.Debt != null && e.Debt.IsPaid) : (e.Debt != null && !e.Debt.IsPaid)));

            int skip = (query.PageIndex - 1) * query.PageSize;
            var totalItems = await _context.Set<Expense>().Where(filter).CountAsync();
            var items = await FindAsync(
    filter,
    skip,
    query.PageSize,
    include: q => q.Include(e => e.Debt)
);


            return new PagedResult<Expense>
            {
                Items = items,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                TotalItems = totalItems
            };
        }
        public async Task<List<Expense>> GetExpensesByDebtIdAsync(Guid debtId, ExpenseType expenseType, Guid? excludeExpenseId = null)
        {
            return await _context.Expenses
       .Where(e => e.DebtId == debtId && e.Type == expenseType && (!excludeExpenseId.HasValue || e.Id != excludeExpenseId.Value))
       .ToListAsync();
        }
    }
}