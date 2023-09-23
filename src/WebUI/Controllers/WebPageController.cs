using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class WebPageController : Controller
    {
        private readonly ILogger<WebPageController> _logger;
        private readonly IWebPageService _webPageService;
        private readonly IPagesService _pagesService;
        private const int Page_Size = 30;

        public WebPageController(
            ILogger<WebPageController> logger,
            IWebPageService webPageService,
            IPagesService pagesService)
        {
            _logger = logger;
            _webPageService = webPageService;
            _pagesService = pagesService;
        }

        #region Home and Pages
        public IActionResult Index([FromQuery] int pageNumber = 0)
        {
            var url = "";
            var webPageVM = _webPageService.CurrentWebPage(url, pageNumber, Page_Size);            
            return View(webPageVM);
        }
        public IActionResult LoadMore(int pageNumber)
        {
            var url = "";
            var webPageVM = _webPageService.CurrentWebPage(url, pageNumber, Page_Size);
            return PartialView("Partials/PageChildren", webPageVM.ChildPages);
        }
        #endregion

        #region tags
        public IActionResult TagsPage(string tagName, [FromQuery] int pageNumber = 0)
        {
            var webPageVM = _webPageService.CurrentWebPageFromTag(tagName, pageNumber, Page_Size);
            return View(webPageVM);
        }
        public IActionResult TagsLoadMore(string tagName, [FromQuery] int pageNumber = 0)
        {
            var webPageVM = _webPageService.CurrentWebPageFromTag(tagName, pageNumber, Page_Size);
            return PartialView("Partials/PageChildren", webPageVM.ChildPages);
        }
        #endregion

        #region Users
        public IActionResult UsersPage(string userName, [FromQuery] int pageNumber = 0)
        {
            var webPageVM = _webPageService.CurrentWebPageFromUser(userName, pageNumber, Page_Size);
            return View(webPageVM);
        }
        public IActionResult UsersLoadMore(string userName, [FromQuery] int pageNumber = 0)
        {
            var webPageVM = _webPageService.CurrentWebPageFromUser(userName, pageNumber, Page_Size);
            return PartialView("Partials/PageChildren", webPageVM.ChildPages);
        }

        #endregion
        public IActionResult InternalPage(string? pageUrl, string? subPageUrl, string? subSubPageUrl)
        {
            var url = subSubPageUrl ?? subPageUrl ?? pageUrl;
            var webPageVM = _webPageService.CurrentWebPage(url, 0, Page_Size);
            return View(webPageVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}