using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Domain.Models;

public class UserProperties
{
    [Key]
    public string UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public string? Instagram { get; set; }
    public string? Facebook { get; set; }
}
