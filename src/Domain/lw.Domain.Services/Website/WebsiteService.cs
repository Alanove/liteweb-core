﻿namespace lw.Domain.Services;

public class WebsiteService : IWebsiteService
{
    #region internal variables
    private readonly Repository<Website> _websites;
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    User? _currentLoggedInUser;
    #endregion

    public WebsiteService(AppDbContext dbContext,
        IHttpContextAccessor httpContextAccessor)
    {
        _context = dbContext;
        _websites = new Repository<Website>(_context);
        _httpContextAccessor = httpContextAccessor;
    }
    public Website? GetWebsite(string domainName)
    {
        return this._websites.Where(w => w.Domain == domainName).FirstOrDefault();
    }
    public Website CurrentWebsite()
    {
        var website = GetWebsite(_httpContextAccessor.HttpContext.Request.Host.Value);
        if(website == null)
        {
            throw new Exception("Website not found for the current domain, please fix the databse or contact support.");
        }
        return website;
    }
    public Website AddWebsite(Website website)
    {
        this._websites.Add(website);
        this._websites.SaveChanges();
        return website;
    }
}
