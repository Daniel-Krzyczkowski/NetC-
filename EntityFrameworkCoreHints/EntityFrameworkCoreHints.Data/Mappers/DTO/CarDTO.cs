using System;
using System.Collections.Generic;
using System.Text;

namespace EntityFrameworkCoreHints.Data.Mappers.DTO
{
    public class CarDTO
    {
        public Guid Id { get; set; }
        public string RegistrationNumber { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Guid OwnerId { get; set; }
    }
}
