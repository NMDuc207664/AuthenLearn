using System.ComponentModel.DataAnnotations;

namespace AuthenLearn.Model.Enum
{
    public enum ExpenseType
    {
        [Display(Name = "Nợ")]
        Debt = 1,

        [Display(Name = "Chi tiêu")]
        Pay = 2,

        [Display(Name = "Thu nhập")]
        Plus = 3,

        [Display(Name = "Trả nợ")]
        PayOffDebt = 4
    }
}