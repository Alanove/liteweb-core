using lw.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace lw.Domain.Services;
public class UsersService : IUsersService
{
    #region internal variables
    private readonly Repository<User> _users;
    private readonly Repository<UserProperties> _userProperties;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    User? _currentLoggedInUser;
    #endregion

    public UsersService(AppDbContext dbContext, 
        IHttpContextAccessor httpContextAccessor,
		IConfiguration configuration)
    {
        _context = dbContext;
        _users = new Repository<User>(_context);
        _userProperties = new Repository<UserProperties>(_context);
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
    }

    public IQueryable<User> GetUsers()
    {
        return this._users.GetAll();
    }
    public UsersService(AppDbContext dbContext,
      IHttpContextAccessor httpContextAccessor)
    {
        _context = dbContext;
        _users = new Repository<User>(_context);
        _userProperties = new Repository<UserProperties>(_context);
        _httpContextAccessor = httpContextAccessor;
    }
    public User AddUser(User user)
    {
  //      var password = user.Password;
  //      user.Password = "";
  //      this._users.Add(user);
  //      this._users.SaveChanges(); //need to generate userid as its used int the hash

		//user.Password = GenerateHash(user, password == null ? "" : password);
  //      this._users.SaveChanges();
        
        return user;
    }
    public UserProperties AddUserWithProperties(UserProperties userWithProperties)
    {
        if(GetUser(userWithProperties.User.UserName) == null)
        {
            userWithProperties.User = AddUser(userWithProperties.User);
        }
        userWithProperties.UserId = userWithProperties.User.Id;
        _userProperties.Add(userWithProperties);
        return userWithProperties;
    }

    public User? GetUser(string? userName)
	{
		return this._users.Where(u => u.Email == userName || u.UserName == userName)
			.FirstOrDefault();
	}

	public bool ValidateCredentials(string userName, string password)
	{
        var user = GetUser(userName);
        //if(user != null)
        //{
        //    return user.Password == GenerateHash(user, password);
        //}
        return false;
	}
    public IQueryable<Page> GetPages(User user)
    {
        var query = _context.Pages
            .FromSqlRaw<Page>($"select * from \"Pages\" where \"CreatedBy\"='{user.Id}'");
        return query;
    }
    #region helpers
    public string GenerateHash(User user, string password)
    {
        string salt = "MyBallout";
        //try
        //{
        //    salt = _configuration.GetSection("Security").GetValue<string>("Salt");

        //}
        //catch
        //{
        //}
        return Hash.GetHash($"{password}{user.Id}{salt}", Hash.HashType.SHA512);
	}
    #endregion
    #region properties

    public User? CurrentLoggedInUser
    {
        get
        {
            if (_currentLoggedInUser == null)
            {
                string? email = _httpContextAccessor.HttpContext.User.Identity?.Name;
                if (email != null)
                    _currentLoggedInUser = GetUser(email);
            }
            return _currentLoggedInUser;
        }
    }
    #endregion
}
