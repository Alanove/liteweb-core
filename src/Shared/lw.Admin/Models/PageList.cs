using lw.Core.Cte.Enum;

namespace lw.Admin.Models;
public class PageList(AdminModel adminModel): AdminModel(adminModel)
{
	public Guid? ParentId { get; set; } = null;
	public PageStatus? PageStatus { get; set; }
	public List<PageListDTO> Pages { get; set; } = default!;
}