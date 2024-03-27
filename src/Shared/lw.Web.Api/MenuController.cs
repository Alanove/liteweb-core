namespace lw.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MenuController : ControllerBase
{
	private readonly ILogger<MenuController> _logger;
	private readonly IMenuService _menuService;

	public MenuController(ILogger<MenuController> logger, 
		IMenuService menuService)
	{
		_logger = logger;
		_menuService = menuService;
	}
    [HttpGet]
    public List<Menu> GetAllMenus()
    {
        return _menuService.AllMenus().ToList();
    }
    [HttpPut] 
	public void Put(Menu menu)
	{
		_menuService.AddMenu(menu);
	}
}