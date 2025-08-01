using AuthenLearn.Applications.DTOs.Response;
using AuthenLearn.Applications.Extensions;
using AuthenLearn.Applications.Helpers;
using AuthenLearn.Applications.Interfaces;
using AuthenLearn.Applications.Interfaces.ServicesInterfaces;
using AuthenLearn.Data.Common;
using AuthenLearn.Data.Common.QueryParameters;
using AuthenLearn.Model;
using AuthenLearn.Model.Enum;

namespace AuthenLearn.Applications.Services
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _iExpenseRepository;
        public ExpenseService(IExpenseRepository iExpenseRepository)
        {
            _iExpenseRepository = iExpenseRepository;
        }

        public void AddExpense(Expense request, Guid userId)
        {
            var expense = new Model.Expense
            {
                Description = request.Description,
                Date = request.Date,
                Amount = request.Amount,
                UserId = userId,
                Type = request.Type
            };
            if (request.Type == ExpenseType.Debt || request.Type == ExpenseType.PayOffDebt)
            {
                // Gán DebtId từ request
                expense.DebtId = request.DebtId;
            }

            _iExpenseRepository.Add(expense);

        }


        public async Task DeleteExpense(Guid expenseId)
        {
            var expense = await _iExpenseRepository.GetByIdAsync(expenseId);
            if (expense == null) return;
            // if (expense.Type == ExpenseType.Debt)
            // {
            //     var debt = await _iDebtRepository.GetByIdAsync(expense.DebtId.Value);
            //     if (debt != null)
            //     {
            //         var payOffs = await _iExpenseRepository.GetExpensesByDebtIdAsync(debt.Id, ExpenseType.PayOffDebt, excludeExpenseId: expense.Id);

            //         if (payOffs.Any())
            //         {
            //             throw new InvalidOperationException("Không thể xóa khoản nợ vì đã có chi tiêu trả nợ.");
            //         }

            //         _iDebtRepository.Delete(debt);
            //     }
            // }
            _iExpenseRepository.Delete(expense);
        }

        public Task<MoneyResponse> GetAllExpensesByCondition(Guid userId, string key, DateTime? time)
        {
            switch (key)
            {
                case "spent_year":
                    if (time == null) return Task.FromResult(new MoneyResponse());
                    return GetAllExpensesPayByYear(userId, time.Value.Year);
                case "spent_month_year":
                    if (time == null) return Task.FromResult(new MoneyResponse());
                    return GetAllExpensesPayByMonth(userId, time.Value.Year, time.Value.Month);
                case "remaining_debt_by_year":
                    if (time == null) return Task.FromResult(new MoneyResponse());
                    return GetAllRemainingDebtByYear(userId, time.Value.Year);
                case "remaining_debt_by_month_year":
                    if (time == null) return Task.FromResult(new MoneyResponse());
                    return GetAllRemainingDebtByMonth(userId, time.Value.Year, time.Value.Month);
                // case "remaining_debt":
                //     return GetAllExpensesByMonth(userId, time.Value.Year, time.Value.Month);
                // case "remaining_money":
                //     return GetAllExpensesByMonth(userId, DateTime.Now.Year, DateTime.Now.Month);
                default:
                    return Task.FromResult(new MoneyResponse());
            }
        }

        public async Task<MoneyResponse> GetAllExpensesPayByMonth(Guid userId, int year, int month)
        {
            var query = new ExpenseQueryParameter
            {
                Year = year,
                Month = month
            };
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            var TotalAmount = result.Items
            .Where(r => r.Type == ExpenseType.Pay || r.Type == ExpenseType.PayOffDebt)
            .Sum(r => r.Amount);
            return TotalAmount.ToMoneyResponse();
        }

        public async Task<MoneyResponse> GetAllExpensesPayByYear(Guid userId, int year)
        {
            var query = new ExpenseQueryParameter
            {
                Year = year
            };
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            var TotalAmount = result.Items
            .Where(r => r.Type == ExpenseType.Pay || r.Type == ExpenseType.PayOffDebt)
            .Sum(r => r.Amount);
            return TotalAmount.ToMoneyResponse();
        }

        public async Task<MoneyResponse> GetAllRemainingDebtByMonth(Guid userId, int year, int month)
        {
            var query = new ExpenseQueryParameter
            {
                Year = year,
                Month = month
            };
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            var unpaidDebtItems = result.Items
            .Where(e => e.Type == ExpenseType.Debt && e.Debt != null && !e.Debt.IsPaid)
            .ToList();

            var unpaidDebtIds = unpaidDebtItems
            .Select(e => e.DebtId)
            .Distinct()
            .ToList();
            var totalUnpaidDebt = unpaidDebtItems
            .Select(e => e.Debt!.TotalAmountOfDebt) // safe because filtered by e.Debt != null
            .Distinct()
            .Sum();
            var totalPaid = result.Items
            .Where(e =>
                e.Type == ExpenseType.Pay ||
                (e.Type == ExpenseType.PayOffDebt && e.DebtId != null && unpaidDebtIds.Contains(e.DebtId.Value))
            )
            .Sum(e => e.Amount);
            var remaining = totalPaid - totalUnpaidDebt;
            return remaining.ToMoneyResponse();
        }

        public async Task<MoneyResponse> GetAllRemainingDebtByYear(Guid userId, int year)
        {
            var query = new ExpenseQueryParameter
            {
                Year = year
            };
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            var unpaidDebtItems = result.Items
            .Where(e => e.Type == ExpenseType.Debt && e.Debt != null && !e.Debt.IsPaid)
            .ToList();
            var unpaidDebtIds = unpaidDebtItems
           .Select(e => e.DebtId)
           .Distinct()
           .ToList();
            var totalUnpaidDebt = unpaidDebtItems
           .Select(e => e.Debt!.TotalAmountOfDebt)
           .Distinct()
           .Sum();
            var totalPaid = result.Items
            .Where(e =>
                e.Type == ExpenseType.Pay ||
                (e.Type == ExpenseType.PayOffDebt && unpaidDebtIds.Contains(e.DebtId))
            )
            .Sum(e => e.Amount);
            var remaining = totalPaid - totalUnpaidDebt;
            return remaining.ToMoneyResponse();
        }

        public async Task<PagedResult<Expense>> GetBookByPagination(Guid userId, ExpenseQueryParameter query)
        {
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            result.Title = TitleUpdateHelper.GetTitleFromQuery(query);
            return result;
        }

        public async Task UpdateExpense(Expense request)
        {
            var expense = await _iExpenseRepository.GetByIdAsync(request.Id);
            if (expense != null)
            {
                if (expense.Type != ExpenseType.PayOffDebt)
                {
                    expense.Description = request.Description;
                    expense.Date = request.Date;
                    expense.Amount = request.Amount;
                }
                else if (expense.Type == ExpenseType.PayOffDebt)
                {
                    expense.Description = request.Description;
                    expense.Date = request.Date;
                    expense.Amount = request.Amount;
                    expense.DebtId = request.DebtId;
                }
                _iExpenseRepository.Update(expense);
            }
        }
    }
}