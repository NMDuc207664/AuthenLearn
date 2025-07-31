using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenLearn.Model;

namespace AuthenLearn.Applications.Interfaces.ServicesInterfaces
{
    public interface IAccountService
    {
        void AddExpense(Debt request, Guid userId);
        Task UpdateExpense(Debt request);
        Task DeleteExpense(Guid expenseId);
    }
}