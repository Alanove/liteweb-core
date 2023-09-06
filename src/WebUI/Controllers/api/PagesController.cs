namespace lw.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PagesController : ControllerBase
{
	private readonly ILogger<PagesController> _logger;
	private readonly IPagesService _pagesService;

	public PagesController(ILogger<PagesController> logger, IPagesService pagesService)
	{
		_logger = logger;
		_pagesService = pagesService;
	}

	[HttpGet]
	public List<Page> GetAllPages()
	{
		return _pagesService.GetPages().ToList();
	}
	[HttpPut] 
	public void Put(Page page)
	{
		_pagesService.AddPage(page);
	}
}