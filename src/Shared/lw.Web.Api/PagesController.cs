using AutoMapper;

namespace lw.Api;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class PagesController : ControllerBase
{
	private readonly ILogger<PagesController> _logger;
	private readonly IPagesService _pagesService;
    private readonly IMapper _mapper;

    public PagesController(ILogger<PagesController> logger, 
		IPagesService pagesService,
        IMapper mapper)
	{
		_logger = logger;
		_pagesService = pagesService;
        _mapper = mapper;
    }

	[HttpGet]
	public ActionResult<List<PageListDTO>> GetAllPages()
	{
		return Ok(_pagesService.GetPages()
			.Select(p => _mapper.Map<PageListDTO>(p))
		);
	}
    [HttpGet("get-pages")]
    public ActionResult<List<PageListDTO>> GetPages(int pageSize, int pageNumber, Guid? parentPageId = null, string searchTerm = "")
    {
		return Ok(_pagesService.GetPages(pageSize, pageNumber, parentPageId, searchTerm)
			.Select(p => _mapper.Map<PageListDTO>(p))
		);
    }
    [HttpPut] 
	public void Put(Page page)
	{
		_pagesService.AddPage(page);
	}
}