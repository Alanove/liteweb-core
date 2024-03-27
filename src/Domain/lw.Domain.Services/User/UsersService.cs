global using Microsoft.AspNetCore.Identity;
using lw.Domain.Models;
using lw.Infra.DataContext.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace lw.Domain.Services;
public class UsersService(AppDbContext dbContext,
        IHttpContextAccessor httpContextAccessor,
        IConfiguration configuration,
        SignInManager<User> signInManager,
        ILogger<UsersService> logger,
        UserManager<User> userManager) : IUsersService
{
    #region internal variables
    private readonly AppDbContext _context = dbContext;
    private readonly Repository<User> _users = new Repository<User>(dbContext);
    private readonly Repository<UserProperties> _userProperties = new Repository<UserProperties>(dbContext);
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly IConfiguration _configuration = configuration;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly UserManager<User> _userManager = userManager;
    private readonly ILogger<UsersService> _logger = logger;
    User? _currentLoggedInUser;
    #endregion


    public IQueryable<User> GetUsers()
    {
        return this._users.GetAll();
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
        if (GetUser(userWithProperties.User.UserName) == null)
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

    public async Task<SignInResult> TryToSignIn(string userName, string password, bool rememberMe)
    {
        var result = await _signInManager.PasswordSignInAsync(userName, password, rememberMe, lockoutOnFailure: true);
        return result;
    }
    public IQueryable<Page> GetPages(User user)
    {
        var query = _context.Pages
            .FromSql<Page>($"select * from \"Pages\" where \"CreatedBy\"='{user.Id}'");
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
