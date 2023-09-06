namespace lw.Domain.Services;

public class MenuService : IMenuService
{
    #region internal variables
    private readonly Repository<Menu> _menus;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    User? _currentLoggedInUser;
    #endregion

    public MenuService(AppDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = dbContext;
        _menus = new Repository<Menu>(_context);
        _httpContextAccessor = httpContextAccessor;
    }
    public Menu AddMenu(Menu menu)
    {
        if(menu.Url == null)
        {
            menu.Url = StringUtils.ToUrl(menu.Title);
        }
        this._menus.Add(menu);
        this._menus.SaveChanges();
        return menu;
    }
    public Menu? CurrentMenu()
    {
        var request = _httpContextAccessor.HttpContext.Request;
        var path = request.Path.ToString();
        if (path.StartsWith("/"))
        {
            path = path.Substring(1);
        }
        var pathWithQueryString = $"{request.Path}/{request.QueryString}";
        var menu = _menus.Where(m => 
                (m.Url == path) ||
                (m.Url.IndexOf("?") > 0 && m.Url == pathWithQueryString)
            )
            .Include(m => m.Page)
            .FirstOrDefault();
        
        return menu;
    }
    public IEnumerable<Menu> AllMenus()
    {
        return this._menus.GetAll();
    }
}
