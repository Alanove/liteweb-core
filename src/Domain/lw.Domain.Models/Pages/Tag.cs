namespace lw.Domain.Models;

public class Tag : BaseEntity
{
    [MaxLength(50)]
    public string Url { get; set; } = null!;
	[MaxLength(50)]
	public string Name { get; set; } = null!;
    public virtual List<Page>? Pages { get; set; } = null!;
}
