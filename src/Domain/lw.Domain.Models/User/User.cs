using lw.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace lw.Core.Data;

public class User : TrackedEntity
{
    [MaxLength(100)]
    public string? UserName { get; set; } = string.Empty;
	[MaxLength(128)] 
    public string? Password { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
    [MaxLength(256)]
    public string? Name { get; set; } = string.Empty;
    [MaxLength(256)]
    public string? Email { get; set; } = string.Empty;

    public virtual UserProperties UserProperties { get; set; } = null!;

    public virtual ICollection<Page> Pages { get; set; } = null!;
}