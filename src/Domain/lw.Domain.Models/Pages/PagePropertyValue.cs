namespace lw.Domain.Models;

public class PagePropertyValue
{
	public Guid PageId { get; set; }

	public Guid PagePropertiesId { get; set; }

    public virtual PageProperties Property { get; set; } = null!;
	[MaxLength(256)]
	public string Value { get; set; } = null!;
}
