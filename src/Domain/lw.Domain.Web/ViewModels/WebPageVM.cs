using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Domain.Web;

public class WebPageVM
{
    string? _title = null,
        _description = null;
    public Website Website { get; set; } = null!;
    public Page? CurrentPage { get; set; } = null!;
    public Menu? CurrentMenu { get; set; }  = null!;

    public PagesListVM ChildPages { get; set; } = null!;

    public string PageTitle
    {
        get
        {
            return $"{Title}{(Title == null? "": " - ")}{Website?.Name}";
        }
        
    }
    public string? Title
    {
        get
        {
            if (_title != null)
                return _title;
            if(CurrentPage != null && CurrentPage.Title != null)
                _title = CurrentPage.Title;

            return _title;
        }
        set
        {
            _title = value;
        }
    }
    public string? Description
    {
        get
        {
            if (_description == null)
            {
                if (CurrentPage != null && !String.IsNullOrWhiteSpace(CurrentPage.Description))
                {
                    _description = CurrentPage.Description;
                }
                else
                {
                    _description = Website.Description;
                }
            }
            return _description;
        }
        set
        {
            _description = value;
        }
    }
}
