namespace lw.Core.Cte.Enum;
/// <summary>
/// An enumeration representing the possible states of database records.
/// These states include active records and records marked as deleted (soft delete).
/// </summary>
public enum DbState
{
	/// <summary>
	/// Active database record.
	/// </summary>
	Active = 1,

	/// <summary>
	/// Soft-deleted database record.
	/// </summary>
	Deleted = 2
}