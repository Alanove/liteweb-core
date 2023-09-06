namespace lw.Domain.Models;

public class WebsiteSettings : BaseEntity
{
    [MaxLength(30)]
    public string Name { get; set; } = null!;
	public string Setting { get; set; } = null!;
}