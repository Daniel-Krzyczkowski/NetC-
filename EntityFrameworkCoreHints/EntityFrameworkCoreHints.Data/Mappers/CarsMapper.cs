using EntityFrameworkCoreHints.Data.Mappers.DTO;
using EntityFrameworkCoreHints.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace EntityFrameworkCoreHints.Data.Mappers
{
    public interface ICarsMapper
    {
        Expression<Func<Car, CarDTO>> MapToDTO { get; }
    }

    public class CarsMapper : ICarsMapper
    {
        public Expression<Func<Car, CarDTO>> MapToDTO { get; }

        public CarsMapper()
        {
            MapToDTO = c => new CarDTO
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                OwnerId = c.OwnerId,
                RegistrationNumber = c.RegistrationNumber
            };
        }
    }
}
