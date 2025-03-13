using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authentication.Domain.Entities;
public class EmailVerificationToken
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();
    [ForeignKey(nameof(User))]
    public int UserId { get; set; }
    public DateTime CreatedOnUtc { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresOnUtc { get; set; } = DateTime.UtcNow.AddDays(1);
    public User User { get; set; }
}
