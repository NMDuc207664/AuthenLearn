using AuthenLearn.Applications.Interfaces;
using AuthenLearn.Applications.Interfaces.ServicesInterfaces;
using AuthenLearn.Model;
using AuthenLearn.Model.Enum;

namespace AuthenLearn.Applications.Services
{
    public class DebtService : IDebtService
    {
        private readonly IDebtRepository _iDebtRepository;

        public DebtService(IDebtRepository iDebtRepository)
        {
            _iDebtRepository = iDebtRepository;
        }
        public Guid AddDebt(Debt request, Guid userId)
        {
            var debt = new Debt
            {
                Owner = request.Owner,
                Description = request.Description,
                TotalAmountOfDebt = request.TotalAmountOfDebt,
                UserId = userId
            };
            _iDebtRepository.Add(debt);
            return debt.Id;
        }

        public async Task DeleteDebt(Guid debtId)
        {
            var debt = await _iDebtRepository.GetByIdAsync(debtId);
            if (debt != null)
            {
                _iDebtRepository.Delete(debt);
            }
        }

        public async Task<List<Debt>> GetAllUserDebt(Guid userId, bool isPaid = false)
        {
            var allDebt = await _iDebtRepository.GetAllUserDebt(userId);
            if (isPaid)
            {
                var paidDebts = allDebt.Where(d => d.IsPaid).ToList();
                Console.WriteLine($"Paid debts: {paidDebts.Count}");
                return paidDebts;
            }
            else
            {
                var unpaidDebts = allDebt.Where(d => !d.IsPaid).ToList();
                Console.WriteLine($"Unpaid debts: {unpaidDebts.Count}");
                return unpaidDebts;
            }
        }

        public async Task UpdateDebtPaid(Guid debtId)
        {
            var debt = await _iDebtRepository.GetByIdAsync(debtId);
            var totalPaid = debt.Expenses?
                   .Where(e => e.DebtId == debtId && e.Type == ExpenseType.PayOffDebt)
                   .Sum(e => e.Amount) ?? 0;
            if (totalPaid >= debt.TotalAmountOfDebt)
            {
                debt.IsPaid = true;
            }
            _iDebtRepository.Update(debt);
        }

        public async Task UpdateDebt(Debt request)
        {
            var debt = await _iDebtRepository.GetByIdAsync(request.Id);
            if (debt != null)
            {
                debt.Description = request.Description;
                debt.Owner = request.Owner;
                debt.TotalAmountOfDebt = request.TotalAmountOfDebt;
            }
            else
            {
                return;
            }
            _iDebtRepository.Update(debt);
        }
    }
}