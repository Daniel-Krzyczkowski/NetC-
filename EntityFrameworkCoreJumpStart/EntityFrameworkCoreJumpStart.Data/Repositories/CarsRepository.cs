using EntityFrameworkCoreJumpStart.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreJumpStart.Data.Repositories
{
    public class CarsRepository : IGenericRepository<Car>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CarsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
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
            return await _applicationDbContext.Cars
               .Where(x => x.Id == id)
               .SingleOrDefaultAsync();
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
    }
}
