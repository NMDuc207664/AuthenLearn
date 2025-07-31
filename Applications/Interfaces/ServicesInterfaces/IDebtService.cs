using AuthenLearn.Model;

namespace AuthenLearn.Applications.Interfaces.ServicesInterfaces
{
    public interface IDebtService
    {
        Guid AddDebt(Debt request, Guid userId);
        Task UpdateDebt(Debt request);
        Task DeleteDebt(Guid debtId);
        Task<List<Debt>> GetAllUserDebt(Guid userId, bool isPaid = false);
        Task UpdateDebtPaid(Guid debtId);
    }
}