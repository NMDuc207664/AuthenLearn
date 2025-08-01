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
        public override async Task<Debt> GetByIdAsync(Guid id)
        {
            var entity = await _context.Debts.Include(d => d.Expenses).FirstOrDefaultAsync(d => d.Id == id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity of type {typeof(Debt).Name} with ID '{id}' was not found.");
            }
            return entity;
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