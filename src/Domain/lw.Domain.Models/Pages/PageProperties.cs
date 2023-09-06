namespace lw.Domain.Models;

public class PageProperties : BaseEntity
{
	[MaxLength(50)]
	public string Name { get; set; } = null!;
}
