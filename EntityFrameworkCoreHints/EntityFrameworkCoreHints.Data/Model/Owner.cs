using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreHints.Data.Model
{
    public class Owner : IEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }
    }
}
