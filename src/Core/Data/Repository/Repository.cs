using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace lw.Core.Data;
public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
	protected DbContext _context;
	protected DbSet<TEntity> _entities;
	
	public Repository(DbContext context)
	{
		_context = context;
		_entities = context.Set<TEntity>();
	}
	
	public DbSet<TEntity> Entities => _entities;

	public virtual void Add(TEntity entity)
	{
		_entities.Add(entity);
	}

	public virtual void AddAsync(TEntity entity)
	{
		_entities.AddAsync(entity);
	}

	public virtual void AddRange(IEnumerable<TEntity> entities)
	{
		_entities.AddRange(entities);
	}
    public virtual void UpdateRange(IEnumerable<TEntity> entities)
    {
        _entities.UpdateRange(entities);
    }
    public virtual void AddRangeAsync(IEnumerable<TEntity> entities)
	{
		_entities.AddRangeAsync(entities);
	}

	public virtual void Update(TEntity entity)
	{
		_entities.Update(entity);
	}

	public virtual void UpdateField(TEntity entity, string Field)
		=> UpdateFields(entity, new string[] { Field });

	public virtual void UpdateFields(TEntity entity, string[] Fields)
	{
		var entry = _context.Entry(entity);

		foreach (PropertyInfo propertyInfo in entity.GetType().GetProperties())
		{
			try
			{
				var property = entry.Property(propertyInfo.Name);

				if (Fields.Contains(propertyInfo.Name))
				{
					property.IsModified = true;
				}
				else
				{
					property.IsModified = false;
				}
			}
			catch
			{
			}
		}
	}
	public virtual void Delete(TEntity entity)
	{
		_entities.Remove(entity);
	}

	public virtual void SoftDelete(TEntity entity)
	{
		throw new NotImplementedException("Soft Delete can only be used in TrackedRepository");
	}

	public virtual int Count() => _entities.Count();

	public virtual IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate) => Find(predicate);

	public virtual IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
		=> _entities.Where(predicate);

	public virtual IQueryable<TEntity> WhereNoTracking(Expression<Func<TEntity, bool>> predicate)
		=> FindNoTracking(predicate);

	public virtual IQueryable<TEntity> FindNoTracking(Expression<Func<TEntity, bool>> predicate)
		=> _entities.AsNoTracking().Where(predicate);

	public virtual ValueTask<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate)
		=> _entities.FindAsync(predicate);

	public virtual TEntity? GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate)
		=> _entities.FirstOrDefault(predicate);

	public virtual Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
		=> _entities.FirstOrDefaultAsync(predicate);

	public virtual TEntity? Get(Guid id) => _entities.Find(id);
	public virtual ValueTask<TEntity?> GetAsync(Guid id) => _entities.FindAsync(id);

	public virtual IQueryable<TEntity> GetAll() => _entities;

	public virtual Task<List<TEntity>> GetAllAsync() => _entities.ToListAsync();

	public void SaveChanges() => _context.SaveChanges();
}