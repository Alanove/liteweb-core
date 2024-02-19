using lw.Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace lw.Core.Data;

public class User : IdentityUser
{
    public UserStatus Status { get; set; }
    [MaxLength(256)]
    public string? Name { get; set; } = string.Empty;

    public virtual UserProperties UserProperties { get; set; } = null!;

    public virtual ICollection<Page> Pages { get; set; } = null!;
}