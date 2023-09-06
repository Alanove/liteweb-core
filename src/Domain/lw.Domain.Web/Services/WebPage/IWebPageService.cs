namespace lw.Domain.Web;

public interface IWebPageService
{
    WebPageVM CurrentWebPage(string? pageUrl = null, int pageNumber = 0, int pageSize = 30);
}
