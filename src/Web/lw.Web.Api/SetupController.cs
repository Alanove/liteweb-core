using Microsoft.EntityFrameworkCore;

namespace lw.Api;
[ApiController]
[Route("[controller]")]
[Authorize]
public class SetupController : ControllerBase
{
    private readonly ILogger<SetupController> _logger;
    private readonly IUsersService _usersService;
    private readonly IPagesService _pagesService;
    private readonly IWebsiteService _websitesService;
    private readonly IMenuService _menuService;
    private readonly AppDbContext _dbContext;

    public SetupController(ILogger<SetupController> logger,
        IUsersService usersService,
        IPagesService pagesService,
        IWebsiteService websiteService,
        IMenuService menuService,
        AppDbContext dbContext)
    {
        _logger = logger;
        _usersService = usersService;
        _pagesService = pagesService;
        _websitesService = websiteService;
        _menuService = menuService;
        _dbContext = dbContext;
    }

    [HttpPut("init-website")]
    public void InitWebsite(string websiteName, string domainName)
    {
        var homePage = new Page
        {
            Title = "Home",
            Content = @"<h1>Welcome to Our Website</h1>
					<p>Your One-Stop Destination for All Things Awesome!</p>
					#beautifulwebsite #randomtag",
        };
        homePage = _pagesService.AddPage(homePage);
        var aboutUs = new Page
        {
            Title = "About Us",
            Content = @"< <h2>About Us</h2>
  <p>Welcome to Our Company, where passion meets innovation.</p>
  <p>At Our Company, we believe in pushing boundaries and redefining possibilities. With a team of dedicated professionals who are driven by creativity and a commitment to excellence, we have set out on a mission to make a difference.</p>
  <p>Our journey began with a vision to create products that inspire and services that empower. We are fueled by a relentless pursuit of quality, and our efforts are guided by the values of integrity, collaboration, and customer-centricity.</p>
  <p>As a company, we embrace challenges as opportunities for growth and embrace change as a catalyst for progress. With every step we take, we are making strides towards a future that is brighter, more innovative, and filled with endless possibilities.</p>
  <p>Thank you for being a part of our journey. Together, we are shaping the future.</p>",
        };
        aboutUs = _pagesService.AddPage(aboutUs);
        var blog = new Page
        {
            Title = "Blog"
        };
        blog = _pagesService.AddPage(blog);
        var blog1 = new Page
        {
            Parent = blog,
            Title = "Blog 1",
            Content = @"<p>As the sun rises over the horizon, a world of beauty and mystery awakens. Nature, with all its enchanting elements, invites us to step outside and embrace its wonders.</p>
    <p>From the vibrant hues of a blooming meadow to the tranquil melodies of a forest stream, every corner of nature has a story to tell. The rustling leaves, the dancing fireflies, the intricate patterns on a butterfly's wings � each detail is a chapter in the book of life.</p>
    <p>Exploring the outdoors is not just an activity; it's an experience that nurtures the soul. It's a chance to disconnect from the digital world and reconnect with the earth beneath our feet. The scent of damp earth after a rain, the taste of wild berries, the sensation of cool water on a hot day � these sensations remind us of the simple joys that surround us.</p>
    <p>As we journey through the wilderness, we discover that nature is a masterful artist, painting landscapes that stir emotions and spark inspiration. It's a teacher that imparts lessons of resilience, adaptation, and the interconnectedness of all living beings.</p>
    <p>So, let us embark on an adventure, not only to witness the wonders of nature but to also recognize our role as stewards of this incredible planet. Through our actions and choices, we can preserve the beauty we admire and ensure that future generations can continue to explore and be inspired.</p>"
        };
        _pagesService.AddPage(blog1);
        var blog2 = new Page
        {
            Parent = blog,
            Title = "Blog 2",
            Content = @"<p>As the sun rises over the horizon, a world of beauty and mystery awakens. Nature, with all its enchanting elements, invites us to step outside and embrace its wonders.</p>
    <p>From the vibrant hues of a blooming meadow to the tranquil melodies of a forest stream, every corner of nature has a story to tell. The rustling leaves, the dancing fireflies, the intricate patterns on a butterfly's wings � each detail is a chapter in the book of life.</p>
    <p>Exploring the outdoors is not just an activity; it's an experience that nurtures the soul. It's a chance to disconnect from the digital world and reconnect with the earth beneath our feet. The scent of damp earth after a rain, the taste of wild berries, the sensation of cool water on a hot day � these sensations remind us of the simple joys that surround us.</p>
    <p>As we journey through the wilderness, we discover that nature is a masterful artist, painting landscapes that stir emotions and spark inspiration. It's a teacher that imparts lessons of resilience, adaptation, and the interconnectedness of all living beings.</p>
    <p>So, let us embark on an adventure, not only to witness the wonders of nature but to also recognize our role as stewards of this incredible planet. Through our actions and choices, we can preserve the beauty we admire and ensure that future generations can continue to explore and be inspired.</p>"
        };
        _pagesService.AddPage(blog2);
        var contactUs = new Page
        {
            Title = "Contact Us",
            Content = @"<h2>Contact Us</h2>
  <p>We'd love to hear from you! Whether you have questions, feedback, or just want to say hello, don't hesitate to get in touch.</p>
  <p>You can reach us by email, phone, or through our social media channels. Our team is ready to assist you and provide the information you need.</p>
  <p>Feel free to fill out the contact form below, and we'll get back to you as soon as possible:</p>
  
  <form>
    <label for=""name"">Name:</label>
    <input type=""text"" id=""name"" name=""name"" required>
    
    <label for=""email"">Email:</label>
    <input type=""email"" id=""email"" name=""email"" required>
    
    <label for=""message"">Message:</label>
    <textarea id=""message"" name=""message"" rows=""4"" required></textarea>
    
    <button type=""submit"">Send Message</button>
  </form>"
        };
        contactUs = _pagesService.AddPage(contactUs);
        var mainMenu = new Menu
        {
            Title = "Manin Menu",
            Children = new List<Menu>
            {
                new Menu
                {
                    Title = homePage.Title,
                    Url = homePage.Url,
                    Page = homePage,
                    Sorting = 1000
                },
                new Menu
                {
                    Title = aboutUs.Title,
                    Url = aboutUs.Url,
                    Page = aboutUs,
                    Sorting = 2000
                },
                new Menu
                {
                    Title = blog.Title,
                    Url = blog.Url,
                    Page = blog,
                    Sorting = 3000,
                    Inheritance = true
                },
                new Menu
                {
                    Title = contactUs.Title,
                    Url = contactUs.Url,
                    Page = contactUs,
                    Sorting = 4000
                }
            }
        };
        _menuService.AddMenu(mainMenu);
        _websitesService.AddWebsite(
            new Website
            {
                Name = websiteName,
                Domain = domainName,
                HeaderMenu = mainMenu,
                FooterMenu = mainMenu,
                HomePage = homePage
            });
    }

    [HttpPut("apply-migrations")]
    public void UpdateMigrations(string password)
    {
        if(password != "UnicornPizza42!Rainbows")
        {
            return;
        }
        _dbContext.Database.Migrate();
    }
}