using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenLearn.Model
{

    [Table("user_account")]
    public class UserAccount
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public Guid Id { get; set; }
        [Column("user_name")]
        [MaxLength(100)]
        public string? UserName { get; set; }
        [Column("password")]
        [MaxLength(100)]
        public string? Password { get; set; }
        public string? Name { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<Expense> Expenses { get; set; } = new List<Expense>();
        public ICollection<Debt> Debts { get; set; } = new List<Debt>();
    }
}