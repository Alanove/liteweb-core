namespace lw.Domain.Web;

public interface IWebPageService
{
    WebPageVM CurrentWebPage(string? pageUrl = null, int pageNumber = 0, int pageSize = 30);
    WebPageVM CurrentWebPageFromTag(string tagName, int pageNumber = 0, int pageSize = 30);
}
