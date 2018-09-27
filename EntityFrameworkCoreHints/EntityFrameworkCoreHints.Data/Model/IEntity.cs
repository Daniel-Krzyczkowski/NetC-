using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreHints.Data.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
