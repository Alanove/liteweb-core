using lw.Core.Cte.Enum;

namespace lw.Admin.Services;

public interface IAdminPagesService
{
	public PageList GetPages(PageList adminViewModel);
}