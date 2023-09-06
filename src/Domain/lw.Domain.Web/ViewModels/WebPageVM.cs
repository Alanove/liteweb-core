using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Domain.Web;

public class WebPageVM
{
    public Website Website { get; set; } = null!;
    public Page? CurrentPage { get; set; } = null!;
    public Menu? CurrentMenu { get; set; }  = null!;

    public PagesListVM ChildPages { get; set; } = null!;

    public string PageTitle
    {
        get
        {
            return $"{CurrentPage?.Title}{(CurrentPage == null? "": " - ")}{Website?.Name}";
        }
    }
    public string Title
    {
        get
        {
            return CurrentPage?.Title;
        }
    }
    public string? Description
    {
        get
        {
            if(CurrentPage != null && !String.IsNullOrWhiteSpace(CurrentPage.Description))
            {
                return CurrentPage.Description;
            }
            return Website.Description;
        }
    }
}
