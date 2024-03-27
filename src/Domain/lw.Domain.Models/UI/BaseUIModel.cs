using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Domain.Models;

public class BaseUIModel
{
    public List<string> ErrorMessages { get; set; } = new List<string>();
    public List<string> WarningMessages { get; set;} = new List<string>();
    public List<string> SuccessMessages { get; set; } = new List<string>();
    public List<string> InfoMessages { get; set; } = new List<string>();
}
