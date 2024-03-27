using lw.Domain.Models;

namespace lw.Domain.Services;
public class PagesService : IPagesService
{
	#region internal variables
	private readonly TrackedRepository<Page> _pages;
	private readonly Repository<Tag> _tags;
	private readonly Repository<PageProperties> _pageProperties;
	private readonly AppDbContext _context;
	private readonly IHttpContextAccessor _httpContextAccessor;

	User? _currentLoggedInUser;
	#endregion

	public PagesService(AppDbContext dbContext,
		IHttpContextAccessor httpContextAccessor)
	{
		_context = dbContext;
		_pages = new TrackedRepository<Page>(_context);
		_tags = new Repository<Tag>(_context);
		_pageProperties = new Repository<PageProperties>(_context);
		_httpContextAccessor = httpContextAccessor;
	}
	public IQueryable<Page> GetPages()
	{
		return this._pages.GetAll();
	}
    public IQueryable<Page> GetPages(int pageSize, int pageNumber,	Guid? parentPageId = null, string searchTerm = "")
    {
		var pages = GetPages();
		if (parentPageId != null)
			pages = pages.Where(p => p.ParentId == parentPageId.Value);
		if(searchTerm != "")
		{
			pages = pages.Where(p => p.Title.Contains(searchTerm) ||
	            (p.Description != null && p.Description.Contains(searchTerm)) ||
                (p.Keywords != null && p.Keywords.Contains(searchTerm)) ||
                (p.Content != null && p.Content.Contains(searchTerm)));
		}
        return pages
			.Skip(pageSize * pageNumber)
            .Take(pageSize);
    }
    public Page AddPage(Page page)
	{
		UpdatePropertyIds(page);
		this._pages.Add(page);
		page.Url = GetUrl(page);
		page.Tags = GetPageTags(page);
		if (string.IsNullOrEmpty(page.Description) && page.Content != null)
		{
			page.Description = StringUtils.Trankate(page.Content, 256);
		}
		this._pages.SaveChanges();
		return page;
	}
    public List<Page> AddPages(List<Page> pages)
    {
        _pages.AddRange(pages);
        _pages.SaveChanges();
        return pages;
    }
    public List<Page> UpdatePages(List<Page> pages)
    {
        _pages.UpdateRange(pages);
        _pages.SaveChanges();
        return pages;
    }
    public Page UpdatePage(Page page)
	{
		//UpdatePropertyIds(page);
		//page.Url = GetUrl(page);
		//page.Tags = GetPageTags(page);
		this._pages.Update(page);
        this._pages.SaveChanges();
        return page;
	}
	public void DeletePage(Page page)
	{
		this._pages.Delete(page);
		this._pages.SaveChanges();
	}
	public Page? GetPageByUrl(string url)
	{
		return this._pages.Where(p => p.Url == url).FirstOrDefault();
	}
	public List<Tag>? GetPageTags(Page page)
	{
		if (page.Content != null)
		{
			if (page.Tags == null)
				page.Tags = new List<Tag>();
			//Extract hash tags from content
			string[] hashTags = StringUtils.ExtractHashtags(page.Content);
			//Adding all tags that are already saved in database
			page.Tags.AddRange(this._tags.Where(t => hashTags.Contains(t.Name)).ToList());
            foreach (Tag tag in page.Tags)
            {
				if (string.IsNullOrEmpty(tag.Url))
					tag.Url = StringUtils.ToUrl(tag.Name);
            }
            //Adding all the rest
            foreach (string tag in hashTags)
			{
				if(page.Tags.Where(t => t.Name == tag).Count() == 0)
					page.Tags.Add(new Tag { Name = tag, Url = StringUtils.ToUrl(tag) });
			}
		}
		return page.Tags;
	}
	#region helper
	void UpdatePropertyIds(Page page)
	{
		if(page.PageProperties != null)
		{
			string[] properties = new string[page.PageProperties.Count()];
			int i = 0;
			//2 Loops to avoid going to databse twice
			//First loop getting all the property names
			foreach(PagePropertyValue propertyValue in page.PageProperties)
			{
				properties[i++] = propertyValue.Property.Name;
			}
			var dbProperties = this._pageProperties.Where(p => properties.Contains(p.Name)).ToList();
			//Second loop updating ids to avoid duplicates
			foreach (PagePropertyValue propertyValue in page.PageProperties)
			{
				var property = dbProperties.Where(p => p.Name == propertyValue.Property.Name).FirstOrDefault();
				//if property not in db, need to create a new one
				if(property != null)
				{
					propertyValue.PagePropertiesId = property.Id;
					propertyValue.Property = property;
				}
				else
				{
					//setting id to avoid db conflict because of keys
					var id = Guid.NewGuid();
					propertyValue.PagePropertiesId = id;
					propertyValue.Property.Id = id;
				}
			}
		}
	}
	string GetUrl(Page page, int i = 0)
	{
		page.Url = $"{StringUtils.ToUrl(page.Title)}{(i != 0? $"-{i}":"")}";
		if(this._pages.Where(p => p.Url == page.Url && p.Id != page.Id).Count() > 0)
		{
			page.Url = GetUrl(page, ++i);
		}
		return page.Url;
	}
	#endregion
}
