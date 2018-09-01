using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFrameworkCoreJumpStart.Data.Model;
using EntityFrameworkCoreJumpStart.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EntityFrameworkCoreJumpStart.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IGenericRepository<Car> _carsRepository;

        public CarsController(IGenericRepository<Car> carsRepository)
        {
            _carsRepository = carsRepository;
        }

        // GET api/cars
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cars = await _carsRepository.AllAsync();
            if (cars == null)
                return NotFound();

            return Ok(cars);
        }

        // GET api/cars/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var car = await _carsRepository.GetAsync(id);
            if (car == null)
                return NotFound();

            return Ok(car);
        }

        // POST api/cars
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Car car)
        {
            var createdCar = await _carsRepository.InsertAsync(car);

            return Created(new Uri($"/api/cars/{createdCar.Id}", UriKind.Relative), createdCar);
        }

        // PUT api/cars/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Car car)
        {
            var existingCar = await _carsRepository.GetAsync(id);
            if (existingCar == null)
                return NotFound();

            car.Id = id;
            existingCar = await _carsRepository.UpdateAsync(car);

            return NoContent();
        }

        // DELETE api/cars/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deletedCar = await _carsRepository.DeleteAsync(id);

            if (!deletedCar)
                return NotFound();

            return NoContent();
        }
    }
}
