using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Domain.Web;

public class PagesListVM
{
    public List<PageListDTO> Pages { get; set; } = null!;
    public int PageSize { get; set; } = 30;
    public int PageNumber { get; set; }
    public int TotalCount { get; set; }
    public bool HasMore { get; set; } = false;
}
