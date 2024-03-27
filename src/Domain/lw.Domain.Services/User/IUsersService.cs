namespace lw.Domain.Services;

public interface IUsersService
{
	public IQueryable<User> GetUsers();
	public User? AddUser(User user);
	public User? GetUser(string userName);
	public UserProperties AddUserWithProperties(UserProperties properties);
    public Task<SignInResult> TryToSignIn(string userName, string password, bool rememberMe);
    public User? CurrentLoggedInUser { get; }

	public IQueryable<Page> GetPages(User user);
}
