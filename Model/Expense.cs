using System.ComponentModel.DataAnnotations;
using AuthenLearn.Model.Enum;

namespace AuthenLearn.Model
{
    public class Expense
    {
        public Guid Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mô tả là bắt buộc")]
        public string? Description { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Số tiền là bắt buộc")]
        public double Amount { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Ngày là bắt buộc")]
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
        public UserAccount? UserAccount { get; set; }
        public Guid? DebtId { get; set; }
        public Debt? Debt { get; set; }
        public ExpenseType Type { get; set; }
    }
}