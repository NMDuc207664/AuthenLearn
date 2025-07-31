namespace AuthenLearn.Model
{
    public class Debt
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserAccount? UserAccount { get; set; }
        public string Owner { get; set; }
        public double TotalAmountOfDebt { get; set; }
        public string Description { get; set; }
        public bool IsPaid { get; set; } = false;
        public ICollection<Expense>? Expenses { get; set; }
    }
}