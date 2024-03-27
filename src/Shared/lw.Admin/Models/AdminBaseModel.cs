namespace lw.Admin.Models;

public class AdminModel: BaseUIModel
{
	public string? Keyword { get; set; } = null;
	public int PageSize { get; set; } = AdminConstants.DEFAULT_PAGE_SIZE;
	public int PageIndex { get; set; } = 0;
	public int TotalCount { get; set; } = default!;
	public string UserName { get; set; } = "";
	public AdminModel()
	{
	}
	public AdminModel(AdminModel model)
	{
		Keyword = model.Keyword;
		PageSize = model.PageSize;
		PageIndex = model.PageIndex;
		TotalCount = model.TotalCount;
		UserName = model.UserName;
	}
}
