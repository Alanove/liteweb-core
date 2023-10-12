using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace lw.Core.Data;
/// <summary>
/// An interface representing a generic repository for database operations on entities.
/// Provides methods for adding, updating, deleting, and querying entities, as well as various
/// methods for retrieving and manipulating entity data.
/// </summary>
/// <typeparam name="TEntity">The type of entity that the repository works with.</typeparam>
public interface IRepository<TEntity> where TEntity : class
{
	/// <summary>
	/// Adds a new entity to the repository.
	/// </summary>
	void Add(TEntity entity);
	/// <summary>
	/// Asynchronously adds a new entity to the repository.
	/// </summary>
	/// <summary>
	/// Adds a collection of entities to the repository.
	/// </summary>
	void AddRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Asynchronously updates entity to the repository.
    /// </summary>
    /// <summary>
    /// Updates a collection of entities to the repository.
    /// </summary>
    void UpdateRange(IEnumerable<TEntity> entities);

    /// <summary>
    /// Updates an existing entity in the repository.
    /// </summary>
    void Update(TEntity entity);

	/// <summary>
	/// Updates specific fields of an existing entity in the repository.
	/// </summary>
	void UpdateField(TEntity entity, string field);

	/// <summary>
	/// Updates multiple fields of an existing entity in the repository.
	/// </summary>
	void UpdateFields(TEntity entity, string[] fields);

	/// <summary>
	/// Deletes an entity from the repository
	/// </summary>
	void Delete(TEntity entity);

	/// <summary>
	/// If tracked repo, The entity will not be deleted just the state will be updated
	/// </summary>
	void SoftDelete(TEntity entity);

	/// <summary>
	/// Retrieves the total count of entities in the repository.
	/// </summary>
	int Count();
	/// <summary>
	/// Queries the repository using a predicate expression.
	/// </summary>
	/// <summary>
	/// Queries the repository using a predicate expression and returns the results as an IQueryable.
	/// </summary>
	IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Finds entities in the repository that satisfy the provided predicate and returns them as an IQueryable.
	/// </summary>
	IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Queries the repository without tracking changes, using a predicate expression, and returns the results as an IQueryable.
	/// </summary>
	IQueryable<TEntity> WhereNoTracking(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Finds entities in the repository without tracking changes that satisfy the provided predicate and returns them as an IQueryable.
	/// </summary>
	IQueryable<TEntity> FindNoTracking(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Asynchronously finds an entity in the repository that satisfies the provided predicate and returns it.
	/// </summary>
	ValueTask<TEntity?> FindAsync(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Retrieves a single entity from the repository that satisfies the provided predicate, or null if none is found.
	/// </summary>
	TEntity? GetSingleOrDefault(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Asynchronously retrieves a single entity from the repository that satisfies the provided predicate,
	/// or null if none is found.
	/// </summary>
	Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

	/// <summary>
	/// Retrieves an entity from the repository based on its unique identifier, or null if not found.
	/// </summary>
	TEntity? Get(Guid id);

	/// <summary>
	/// Asynchronously retrieves an entity from the repository based on its unique identifier, or null if not found.
	/// </summary>
	ValueTask<TEntity?> GetAsync(Guid id);

    /// <summary>
    /// Retrieves all entities from the repository.
    /// </summary>
    IQueryable<TEntity> GetAll();

	/// <summary>
	/// Asynchronously retrieves all entities from the repository and returns them as a list.
	/// </summary>
	Task<List<TEntity>> GetAllAsync();

	/// <summary>
	/// Saves changes made to the repository.
	/// </summary>
	void SaveChanges();
}