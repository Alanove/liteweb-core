using System.ComponentModel.DataAnnotations.Schema;

namespace lw.Domain.Models;

public class Menu: TrackedEntity
{
    [MaxLength(100)]
    public string Title { get; set; } = null!;
    [MaxLength(256)]
    public string Url { get; set; } = null!;
    public virtual Page? Page { get; set; } = null!;
    public bool? Inheritance { get; set; }
    public int Sorting { get; set; } = 1000;
    public virtual ICollection<Menu> Children { get; set; } = null!;
    [NotMapped]
    public bool IsCurrent { get; set; } = false;
 }
