using AuthenLearn.Applications.DTOs.Response;
using AuthenLearn.Applications.Extensions;
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
        private readonly IDebtRepository _iDebtRepository;
        public ExpenseService(IExpenseRepository iExpenseRepository, IDebtRepository iDebtRepository)
        {
            _iExpenseRepository = iExpenseRepository;
            _iDebtRepository = iDebtRepository;
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
            if (request.Type == ExpenseType.Debt)
            {
                // Gán DebtId từ request
                expense.DebtId = request.DebtId;
            }
            else if (request.Type == ExpenseType.PayOffDebt)
            {
                // Đối với PayOffDebt cũng cần DebtId
                expense.DebtId = request.DebtId;
            }

            _iExpenseRepository.Add(expense);


            // if (request.Type == ExpenseType.Pay || request.Type == ExpenseType.Plus)
            // {
            //     _iExpenseRepository.Add(expense);
            // }
            // else if (request.Type == ExpenseType.Debt)
            // {
            //     var debt = new Debt
            //     {
            //         Owner = request.Debt.Owner,
            //         Description = request.Debt.Description,
            //         TotalAmountOfDebt = request.Amount,
            //         UserId = userId,
            //     };
            //     expense.DebtId = debt.Id;
            //     _iDebtRepository.Add(debt);
            //     _iExpenseRepository.Add(expense);
            // }
            // else if (request.Type == ExpenseType.PayOffDebt)
            // {
            //     expense.DebtId = request.DebtId;
            //     _iExpenseRepository.Add(expense);
            // }
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
                    return GetAllExpensesPayByYear(userId, time.Value.Year);
                case "spent_month_year":
                    return GetAllExpensesPayByMonth(userId, time.Value.Year, time.Value.Month);
                case "remaining_debt_by_year":
                    return GetAllRemainingDebtByYear(userId, time.Value.Year);
                case "remaining_debt_by_month_year":
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
            var unpaidDebtIds = result.Items
            .Where(e => e.Type == ExpenseType.Debt && !e.Debt.IsPaid)
            .Select(e => e.DebtId)
            .Distinct()
            .ToList();
            var totalUnpaidDebt = result.Items
           .Where(e => e.Type == ExpenseType.Debt && !e.Debt.IsPaid)
           .Select(e => e.Debt.TotalAmountOfDebt)
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

        public async Task<MoneyResponse> GetAllRemainingDebtByYear(Guid userId, int year)
        {
            var query = new ExpenseQueryParameter
            {
                Year = year
            };
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            var unpaidDebtIds = result.Items
           .Where(e => e.Type == ExpenseType.Debt && !e.Debt.IsPaid)
           .Select(e => e.DebtId)
           .Distinct()
           .ToList();
            var totalUnpaidDebt = result.Items
           .Where(e => e.Type == ExpenseType.Debt && !e.Debt.IsPaid)
           .Select(e => e.Debt.TotalAmountOfDebt)
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
            throw new NotImplementedException();
        }

        public async Task<PagedResult<Expense>> GetBookByPagination(Guid userId, ExpenseQueryParameter query)
        {
            var result = await _iExpenseRepository.GetByUserIdAsync(userId, query);
            var title = GetTitleFromQuery(query);
            return new PagedResult<Expense>
            {
                Items = result.Items,
                Title = title,
                PageIndex = query.PageIndex,
                PageSize = query.PageSize,
                TotalItems = result.TotalItems
            };
        }

        public async Task UpdateExpense(Expense request)
        {
            var expense = await _iExpenseRepository.GetByIdAsync(request.Id);
            if (expense != null && expense.Type != ExpenseType.PayOffDebt)
            {
                expense.Description = request.Description;
                expense.Date = request.Date;
                expense.Amount = request.Amount;
            }
            else if (expense != null && expense.Type == ExpenseType.PayOffDebt)
            {
                expense.Description = request.Description;
                expense.Date = request.Date;
                expense.Amount = request.Amount;
                expense.DebtId = request.DebtId;
            }
            _iExpenseRepository.Update(expense);
        }


        private string GetTitleFromQuery(ExpenseQueryParameter query)
        {
            var parts = new List<string>();
            if (query.Day.HasValue)
            {
                parts.Add($"ngày: {query.Day.Value}");
            }
            if (query.Month.HasValue)
            {
                parts.Add($"tháng: {query.Month.Value}");
            }
            if (query.Year.HasValue)
            {
                parts.Add($"năm: {query.Year.Value}");
            }
            if (parts.Count == 0)
            {
                return "Toàn bộ danh sách chi tiêu";
            }
            return "Danh sách chi tiêu " + string.Join(" ", parts);
        }
    }
}