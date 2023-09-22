namespace lw.Api;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class UsersController : ControllerBase
{
	private readonly ILogger<UsersController> _logger;
	private readonly IUsersService _usersService;

	public UsersController(ILogger<UsersController> logger, 
		IUsersService usersService)
	{
		_logger = logger;
		_usersService = usersService;
	}

	[HttpPut] 
	public void Put(User user)
	{
		_usersService.AddUser(user);
	}
}