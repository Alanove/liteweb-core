using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lw.Core.Data;

public interface IBaseEntity
{
    /// <summary>
    /// Each Model or table must have a unique Id
    /// </summary>
    Guid Id { get; set; }
}
