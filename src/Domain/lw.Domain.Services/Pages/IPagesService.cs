namespace lw.Domain.Services;

public interface IPagesService
{
	IQueryable<Page> GetPages();
	Page AddPage(Page page);
    List<Page> AddPages(List<Page> pages);
    List<Page> UpdatePages(List<Page> pages);
    Page UpdatePage(Page page);
	void DeletePage(Page page);
	public Page? GetPageByUrl(string url);
	List<Tag> GetPageTags(Page page);
}
