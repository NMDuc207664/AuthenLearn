using AuthenLearn.Applications.Interfaces;
using AuthenLearn.Model;
using AuthenLearn.Model.Enum;
using Microsoft.EntityFrameworkCore;

namespace AuthenLearn.Data.Repositories
{
    public class DebtRepository : GenericRepository<Debt>, IDebtRepository
    {
        private readonly AppDbContext _context;
        public DebtRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Debt>> GetAllUserDebt(Guid userId)
        {
            return await _context.Debts
            .Include(d => d.Expenses)
            .Where(d => d.UserId == userId)
            .ToListAsync();
        }

    }
}