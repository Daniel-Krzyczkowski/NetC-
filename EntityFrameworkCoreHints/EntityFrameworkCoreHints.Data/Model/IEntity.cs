using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreHints.Data.Model
{
    public interface IEntity
    {
        Guid Id { get; set; }
        DateTime CreatedOn { get; set; }
        Guid? CreatedBy { get; set; }
        DateTime ModifiedOn { get; set; }
        Guid? ModifiedBy { get; set; }
    }
}
