namespace lw.Domain.Models;

public class Website: TrackedEntity
{
    [MaxLength(100)]
    public string Domain { get; set; } = null!;
    [MaxLength(100)]
    public string Name { get; set; } = null!;
    [MaxLength(512)]
    public string? Description{ get; set; } = null!;
    [MaxLength(512)]
    public string? Keywords { get; set; } = null!;
    [MaxLength(100)]
    public string? Logo{ get; set; } = null!;
    [MaxLength(100)]
    public string? Image{ get; set; } = null!;
    public List<string>? DomainAliases { get; set; } = null!;
    public virtual Menu? HeaderMenu { get; set; } = null!;
    public virtual Menu? FooterMenu { get; set; } = null!;
    public virtual Page? HomePage { get; set; } = null!;
    public virtual List<WebsiteSettings>? Settings { get; set; } = null!;
    [MaxLength(100)] 
    public string? TemplateCSS { get; set; } = null!;
}
