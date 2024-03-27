namespace lw.Admin.Services;

public class AdminPagesService(AppDbContext dbContext) : IAdminPagesService
{
	private readonly AppDbContext _context = dbContext;
	private readonly TrackedRepository<Page> _pages = new TrackedRepository<Page>(dbContext);
	private readonly Repository<Tag> _tags = new Repository<Tag>(dbContext);
	private readonly Repository<PageProperties> _pageProperties = new Repository<PageProperties>(dbContext);

	public PageList GetPages(PageList pagesVm)
	{
		var query = _pages.GetAll();
		if (pagesVm.ParentId != null)
		{
			query = query.Where(p => p.ParentId == pagesVm.ParentId);
		}
		if (!string.IsNullOrWhiteSpace(pagesVm.Keyword))
		{
			query = query.Where(p => p.Title.Contains(pagesVm.Keyword) || (p.Description != null && p.Description.Contains(pagesVm.Keyword)));
		}
		if (pagesVm.PageStatus != null)
		{
			query = query.Where(p => p.Status == pagesVm.PageStatus);
		}
		pagesVm.TotalCount = query.Count();

		pagesVm.Pages = query.Include(p => p.User)
			.Include(p => p.Parent)
			.OrderByDescending(p => p.PublishDate)
			.ThenByDescending(p => p.DateCreated)
			.Skip(pagesVm.PageSize * pagesVm.PageIndex)
			.Take(pagesVm.PageSize)
			.Select(p => new PageListDTO
			{
				Id = p.Id,
				Title = p.Title,
				Description = p.Description,
				FullUrl = p.FullUrl,
				ThumbImage = p.ThumbImage,
				UserName = p.User.UserName,
				PublishDate = p.PublishDate
			})
			.ToList();
		return pagesVm;
	}
}
