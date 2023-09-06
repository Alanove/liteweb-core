namespace lw.Domain.Services;

public interface IUsersService
{
	public IQueryable<User> GetUsers();
	public User? AddUser(User user);
	public User? GetUser(string userName);
	public UserProperties AddUserWithProperties(UserProperties properties);
	public bool ValidateCredentials(string userName, string password);
    public User? CurrentLoggedInUser { get; }
}
