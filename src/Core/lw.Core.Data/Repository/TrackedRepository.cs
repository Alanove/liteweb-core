using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace lw.Core.Data;
public class TrackedRepository<TEntity> : Repository<TEntity> where TEntity : TrackedEntity
{
	protected Guid? _currentUserId;
	public TrackedRepository(DbContext context): base(context)
	{
		
	}
	public TrackedRepository(DbContext context, Guid? currentUserId)
		: base(context) {
        _currentUserId = currentUserId;
	}
	public override void Add(TEntity entity)
	{
		UpdateRowInfo(entity, DatabaseOperations.Add);
		base.Add(entity);
	}

	public override void AddAsync(TEntity entity)
	{
		UpdateRowInfo(entity, DatabaseOperations.Add);
		base.AddAsync(entity);
	}
	public override void AddRange(IEnumerable<TEntity> entities)
	{
		foreach (TEntity entity in entities)
		{
			UpdateRowInfo(entity, DatabaseOperations.Add);
		}
		base.AddRange(entities);
	}

	public override void AddRangeAsync(IEnumerable<TEntity> entities)
	{
		foreach (TEntity entity in entities)
		{
			UpdateRowInfo(entity, DatabaseOperations.Add);
		}
		base.AddRangeAsync(entities);
	}

	public override void Update(TEntity entity)
	{
		UpdateRowInfo(entity, DatabaseOperations.Update);
		base.Update(entity);
	}
	public override void SoftDelete(TEntity entity)
	{
		UpdateRowInfo(entity, DatabaseOperations.Delete);
		base.Update(entity);
	}
	public override void UpdateFields(TEntity entity, string[] Fields)
	{
		UpdateRowInfo(entity, DatabaseOperations.Update);
		var entry = _context.Entry(entity);

		Array.Resize(ref Fields, Fields.Length + 1);
		Fields[Fields.Length - 1] = "DateModified";

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
	public void UpdateRowInfo(TEntity entity, DatabaseOperations databaseOperation)
	{

		switch (databaseOperation)
		{
			case DatabaseOperations.Add:
				if (_currentUserId != null)
					entity.CreatedBy = _currentUserId;
				break;
			case DatabaseOperations.Update:
				entity.DateModified = DateTime.Now.ToUniversalTime();
				if (_currentUserId != null)
					entity.ModifiedBy = _currentUserId;
				break;
			case DatabaseOperations.Delete:
				entity.DateDeleted = DateTime.Now.ToUniversalTime();
				entity.State = DbState.Deleted;
				if (_currentUserId != null)
					entity.DeletedBy = _currentUserId;
				break;
			default:
				break;
		}
	}
}