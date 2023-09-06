using lw.Domain.Services;
using lw.Domain.Webl;
using Microsoft.EntityFrameworkCore;

namespace lw.Domain.Web;

public class WebPageService : IWebPageService
{
    #region internal variables
    private readonly IPagesService _pagesService;
    private readonly IWebsiteService _websiteService;
    private readonly IMenuService _menuService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    User? _currentLoggedInUser;
    #endregion

    public WebPageService(AppDbContext dbContext,
        IPagesService pagesService,
        IWebsiteService websiteService,
        IMenuService menuService,
        IHttpContextAccessor httpContextAccessor)
    {
        _pagesService = pagesService;
        _websiteService = websiteService;
        _menuService = menuService;
        _httpContextAccessor = httpContextAccessor;
    }

    public WebPageVM CurrentWebPage(string? pageUrl = null, int pageNumber = 0, int pageSize = 30)
    {
        var vm = new WebPageVM
        {
            Website = _websiteService.CurrentWebsite(),
            CurrentMenu = _menuService.CurrentMenu()
        };
        if (vm.CurrentMenu != null && vm.CurrentMenu.Page != null)
        {
            vm.CurrentPage = vm.CurrentMenu.Page;
        }
        else if (pageUrl != null)
        {
            vm.CurrentPage = _pagesService.GetPageByUrl(pageUrl);
        }
        if (vm.CurrentPage != null && vm.CurrentPage.Content != null)
        {
            vm.CurrentPage.Content = StringUtils.InjectHashTagLinks(vm.CurrentPage.Content, "/tags");
        }
        if (vm.CurrentMenu != null)
        {
            if (vm.Website.HeaderMenu != null)
            {
                vm.Website.HeaderMenu.Children = vm.Website.HeaderMenu.Children.OrderBy(m => m.Sorting).ToList();
                SetCurrentMenu(vm.Website.HeaderMenu, vm.CurrentMenu.Id);
            }

            if (vm.Website.FooterMenu != null)
            {
                vm.Website.FooterMenu.Children = vm.Website.FooterMenu.Children.OrderBy(m => m.Sorting).ToList();
                SetCurrentMenu(vm.Website.FooterMenu, vm.CurrentMenu.Id);
            }
        }

        vm.ChildPages = GetChildren(vm.CurrentPage, pageNumber, pageSize);
        return vm;
    }
    public PagesListVM GetChildren(Page? page, int pageNumber, int pageSize)
    {
        var ret = new PagesListVM();
        var query = _pagesService.GetPages()
           .Where(p => p.Status == lw.Core.Cte.Enum.PageStatus.Published);
        if (page != null)
        {
            query = query.Where(p => p.ParentId == page.Id);
        }
        ret.TotalCount = query.Count();
        ret.HasMore = pageNumber * pageSize < ret.TotalCount;

        ret.PageNumber = pageNumber;
        ret.PageSize = pageSize;
        ret.Pages = query.Include(p => p.User)
            .Include(p => p.Parent)
            .OrderByDescending(p => p.PublishDate)
            .ThenByDescending(p => p.DateCreated)
            .Skip(pageSize * pageNumber)
            .Take(pageSize)
            .Select(p => new PagesDTO
            {
                Id = p.Id,
                Title = p.Title,
                FullUrl = p.FullUrl,
                ThumbImage = p.ThumbImage,
                UserName = p.User.UserName,
                PublishDate = p.PublishDate
            })
            .ToList();
    
        return ret;
    }
public bool SetCurrentMenu(Menu menu, Guid currentMenuId)
{
    foreach (Menu childMenu in menu.Children)
    {
        if (childMenu.Id == currentMenuId)
        {
            childMenu.IsCurrent = true;
            return true;
        }
        else
        {
            childMenu.IsCurrent = SetCurrentMenu(childMenu, currentMenuId);
        }
    }
    return false;
}
}
