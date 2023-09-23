namespace lw.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class WebsiteController : ControllerBase
{
	private readonly ILogger<WebsiteController> _logger;
	private readonly IWebsiteService _websiteService;

	public WebsiteController(ILogger<WebsiteController> logger,
        IWebsiteService websiteService)
	{
		_logger = logger;
        _websiteService = websiteService;
	}

	[HttpPut("add-alias")] 
	public bool AddAliasToWebsite(string websiteName, string alias)
	{
		return _websiteService.AddAlias(websiteName, alias);
	}
}