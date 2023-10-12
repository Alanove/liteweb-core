using System.ComponentModel.DataAnnotations;

namespace lw.Core.Data;

public class TrackedEntity : BaseEntity, ITrackedEntity
{
    public Guid? CreatedBy { get; set; } = null;
    public DateTime DateCreated { get; set; } = DateTime.Now.ToUniversalTime();
    public Guid? ModifiedBy { get; set; } = null;
    public DateTime DateModified { get; set; } = DateTime.Now.ToUniversalTime();
    public DbState State { get; set; } = DbState.Active;
    public DateTime? DateDeleted { get; set; }
    public Guid? DeletedBy { get; set; } = null;
}
