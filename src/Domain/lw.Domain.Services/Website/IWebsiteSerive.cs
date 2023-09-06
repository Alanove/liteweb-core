namespace lw.Domain.Services;

public interface IWebsiteService
{
	Website? GetWebsite(string domainName);
	Website CurrentWebsite();
	Website AddWebsite(Website website);
}
