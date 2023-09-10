

using lw.Domain.Models;
using Microsoft.Data.SqlClient;
using Npgsql;

namespace lw.Domain.Services;
public class TagsService : ITagsService
{
	#region internal variables
	private readonly Repository<Tag> _tags;
    private readonly Repository<Page> _pages;
    private readonly AppDbContext _context;
	#endregion

	public TagsService(AppDbContext dbContext,
		IHttpContextAccessor httpContextAccessor)
	{
		_context = dbContext;
		_tags = new Repository<Tag>(_context);
	}

	public Tag? GetTag(string tagName)
	{
		tagName = tagName.ToLower();
		return this._tags.Where(t => t.Name.ToLower() == tagName || t.Url.ToLower() == tagName).FirstOrDefault();
	}
	public List<Tag> AddTags(List<Tag> tags)
	{
		_tags.AddRange(tags);
        _tags.SaveChanges();
        return tags;
    }
	public Tag AddTag(Tag tag)
	{
        if (string.IsNullOrEmpty(tag.Url))
            tag.Url = StringUtils.ToUrl(tag.Name);
		_tags.Add(tag);
		_tags.SaveChanges();
		return tag;
    }
	public IEnumerable<Tag> GetTags()
	{
		return this._tags.GetAll();
	}

	public void DeleteTag(Tag tag)
	{
		this._tags.Delete(tag);
	}

	public IQueryable<Page> GetPages(Tag tag)
	{
        var query = _context.Pages
			.FromSqlRaw<Page>($"select * from \"Pages\" where \"Id\" in (select \"PagesId\" from \"PageTag\" pt where pt.\"TagsId\" = '{tag.Id}') ");
		return query;
	}
}
