using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AuthenLearn.Model
{
    [Table("Role")]
    public class Role
    {
        public int Id { get; init; }
        public string? RoleName { get; init; }
        public ICollection<UserAccount> UserAccount { get; init; } = new List<UserAccount>();
    }
}