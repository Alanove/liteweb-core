using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebUI.Models;

namespace WebUI.Controllers
{
	public class WebPageController : Controller
	{
		private readonly ILogger<WebPageController> _logger;
		private readonly IWebPageService _webPageService;

		public WebPageController(
            ILogger<WebPageController> logger, 
			IWebPageService webPageService)
        {
            _logger = logger;
            _webPageService = webPageService;
        }

        public IActionResult Index(string? pageUrl, string? subPageUrl, string? subSubPageUrl)
		{
			var url = subSubPageUrl ?? subPageUrl ?? pageUrl;
			var webPageVM = _webPageService.CurrentWebPage(url);
			return View(webPageVM);
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}