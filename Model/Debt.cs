using System.ComponentModel.DataAnnotations;

namespace AuthenLearn.Model
{
    public class Debt
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserAccount? UserAccount { get; set; }
        [Required]
        public string Owner { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public double TotalAmountOfDebt { get; set; }
        public bool IsPaid { get; set; } = false;
        public ICollection<Expense>? Expenses { get; set; }
    }
}