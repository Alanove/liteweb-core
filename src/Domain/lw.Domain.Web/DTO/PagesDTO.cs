using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Domain.Webl;

public class PagesDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string? FullUrl { get; set; } = null!;
    public string? ThumbImage { get; set; } = null!;
    public string? UserName { get; set; } = null!;
    public DateTime? PublishDate { get; set; }
}
