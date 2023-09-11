using System.ComponentModel.DataAnnotations.Schema;

namespace lw.Domain.Models;

public class Page: TrackedEntity
{
	public Guid? ParentId { get; set; } = null!;
	public virtual Page? Parent { get; set; }
	[MaxLength(256)]
	public string Url { get; set; } = null!;
	[MaxLength(256)]
	public string Title { get; set; } = null!;
	[MaxLength(1024)]
	public string? Description { get; set; } = null!;
	public string? Content { get; set; } = null!;
	public PageStatus Status { get; set; } = PageStatus.Hidden;
	[MaxLength(256)]
	public string? Image { get; set; } = null!;
    [MaxLength(256)]
    public string? PafeFile { get; set; } = null!;
    public DateTime PublishDate { get; set; } = DateTime.UtcNow;
	public int? Ranking { get; set; } = 10000;
	public int? Views { get; set; } = 0;
	public double? UserRating { get; set; } = 0;
	public string? History { get; set; } = null!;
    public virtual List<Tag>? Tags { get; set; } = null!;
	[MaxLength(256)]
	public string? Keywords { get; set; } = null!;
    public virtual ICollection<PagePropertyValue> PageProperties { get; set; } = null!;

	[NotMapped]    
	public virtual IQueryable<Page> Children { get; set; } = null!;
	public virtual User User { get; set; } = null!;

	[NotMapped]
	public string? ThumbImage
	{
		get
		{
			if (!String.IsNullOrWhiteSpace(Image))
			{
				return $"{Path.GetFileNameWithoutExtension(Image)}-m{Path.GetExtension(Image)}";
			}
			return null;
		}
	}
    [NotMapped]
    public string? LargeImage
    {
        get
        {
            if (!String.IsNullOrWhiteSpace(Image))
            {
                return $"{Path.GetFileNameWithoutExtension(Image)}-l{Path.GetExtension(Image)}";
            }
            return null;
        }
    }
    [NotMapped]
    public string? FullUrl
    {
        get
        {
			if(Parent != null)
				return $"{Parent.FullUrl}/{Url}";

			return Url;
        }
    }

}