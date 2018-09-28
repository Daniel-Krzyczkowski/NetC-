using EntityFrameworkCoreHints.Data.Mappers;
using EntityFrameworkCoreHints.Data.Mappers.DTO;
using EntityFrameworkCoreHints.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreHints.Data.Repositories
{
    public class CarsRepository : IGenericRepository<Car>
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly ICarsMapper _carsMapper;


        public CarsRepository(ApplicationDbContext applicationDbContext, ICarsMapper carsMapper)
        {
            _applicationDbContext = applicationDbContext;
            _carsMapper = carsMapper;
        }

        public async Task<IEnumerable<Car>> AllAsync()
        {
            var cars = await _applicationDbContext.Cars
               .ToListAsync();


            return cars;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var car = await _applicationDbContext.Cars
           .Where(x => x.Id == id)
           .SingleOrDefaultAsync();

            if (car == null)
                return false;

            _applicationDbContext.Cars.Remove(car);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Car> GetAsync(Guid id)
        {
            // All cars will be loaded to the memory first and then Where condition will be applied:
            var enumerableCar = _applicationDbContext.Cars.ToList().Where(x => x.Id == id).SingleOrDefault();

            //Cars table will be filtered on the database side and then specific car record will be returned:
            var queryableCar = _applicationDbContext.Cars.Where(x => x.Id == id).SingleOrDefault();

            // We can use below code to do filtering on the database side:
            var car = await _applicationDbContext.Cars
               .Where(x => x.Id == id)
               .SingleOrDefaultAsync();

            // We can also shorten above query and move condition from Where to SingleOrDefault method:
            return await _applicationDbContext.Cars
               .SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Car> InsertAsync(Car car)
        {
            car.Id = Guid.NewGuid();

            await _applicationDbContext.Cars.AddAsync(car);
            await _applicationDbContext.SaveChangesAsync(true);

            return car;
        }

        public async Task<Car> UpdateAsync(Car car)
        {
            var existingCar = await _applicationDbContext.Cars
               .Where(x => x.Id == car.Id)
               .SingleOrDefaultAsync();

            if (existingCar == null)
                return null;

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;

            await _applicationDbContext.SaveChangesAsync(true);

            return existingCar;
        }

        public async Task<CarDTO> GetCarDTO(Guid id)
        {
            var carDTO = await _applicationDbContext.Cars
              .Where(x => x.Id == id)
              .Select(_carsMapper.MapToDTO)
              .SingleOrDefaultAsync();

            return carDTO;
        }
    }
}
