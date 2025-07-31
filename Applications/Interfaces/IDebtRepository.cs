using AuthenLearn.Model;
using AuthenLearn.Model.Enum;

namespace AuthenLearn.Applications.Interfaces
{
    public interface IDebtRepository : IGenericRepository<Debt>
    {
        Task<List<Debt>> GetAllUserDebt(Guid userId);
    }
}