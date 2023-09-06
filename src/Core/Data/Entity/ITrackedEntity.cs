namespace lw.Core.Data;

/// <summary>
/// Base interface for all Database Models
/// </summary>
public interface ITrackedEntity: IBaseEntity
{
    /// <summary>
    /// Defines who created this entity
    /// </summary>
    public Guid? CreatedBy { get; set; }

    /// <summary>
    /// Created Date 
    /// </summary>
    DateTime DateCreated { get; set; }

    /// <summary>
    /// Defines who modified this entity
    /// </summary>
    public Guid? ModifiedBy { get; set; }

    /// <summary>
    /// Modified Date
    /// </summary>
    DateTime DateModified { get; set; }

    /// <summary>
    /// State of the document can be Active, Updating, Deleted
    /// </summary>
    DbState State { get; set; }

    /// <summary>
    /// Defined who deleted this entity
    /// </summary>
    public Guid? DeletedBy { get; set; }

    /// <summary>
    /// If soft deleted, this would hold the deleted date
    /// </summary>
    DateTime? DateDeleted { get; set; }
}