using lw.Domain.Services;
using lw.Domain.Webl;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace lw.Domain.Web;

public class WebPageService : IWebPageService
{
    #region internal variables
    private readonly IPagesService _pagesService;
    private readonly ITagsService _tagsService;
    private readonly IWebsiteService _websiteService;
    private readonly IMenuService _menuService;
    private readonly IUsersService _usersService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    User? _currentLoggedInUser;
    #endregion

    public WebPageService(AppDbContext dbContext,
        IPagesService pagesService,
        ITagsService tagsService,
        IWebsiteService websiteService,
        IMenuService menuService,
        IHttpContextAccessor httpContextAccessor,
        IUsersService usersService)
    {
        _pagesService = pagesService;
        _tagsService = tagsService;
        _websiteService = websiteService;
        _menuService = menuService;
        _httpContextAccessor = httpContextAccessor;
        _usersService = usersService;
    }

    public WebPageVM CurrentWebPage(string? url = null, int pageNumber = 0, int pageSize = 30)
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
        else if (url != null)
        {
            vm.CurrentPage = _pagesService.GetPageByUrl(url);
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
        var query = _pagesService.GetPages();

        if (vm.CurrentPage != null)
        {
            query = query.Where(p => p.ParentId == vm.CurrentPage.Id);
        }
        vm.ChildPages = GetChildren(query, pageNumber, pageSize);
        return vm;
    }


    public WebPageVM CurrentWebPageFromTag(string tagName, int pageNumber = 0, int pageSize = 30)
    {
        var vm = new WebPageVM
        {
            Website = _websiteService.CurrentWebsite(),
            CurrentMenu = _menuService.CurrentMenu()
        };
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
        var tag = _tagsService.GetTag(tagName);
        if (tag != null)
        {
            vm.Title = "#" + tag.Name;
            vm.ChildPages = GetChildren(_tagsService.GetPages(tag), pageNumber, pageSize);
        }
        return vm;
    }
    public WebPageVM CurrentWebPageFromUser(string userName, int pageNumber = 0, int pageSize = 30)
    {
        var vm = new WebPageVM
        {
            Website = _websiteService.CurrentWebsite(),
            CurrentMenu = _menuService.CurrentMenu()
        };
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
        var user = _usersService.GetUser(userName);
        if (user != null)
        {
            vm.Title = user.UserName;
            vm.ChildPages = GetChildren(_usersService.GetPages(user), pageNumber, pageSize);
        }
        return vm;
    }

    #region Helpers
    public PagesListVM GetChildren(IQueryable<Page> query, int pageNumber, int pageSize)
    {
        var ret = new PagesListVM();
        query = query.Where(p => p.Status == lw.Core.Cte.Enum.PageStatus.Published);
        ret.TotalCount = query.Count();
        ret.HasMore = (pageNumber + 1) * pageSize < ret.TotalCount;

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
    #endregion
}
