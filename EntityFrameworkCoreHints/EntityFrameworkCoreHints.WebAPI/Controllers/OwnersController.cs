using EntityFrameworkCoreHints.Data.Model;
using EntityFrameworkCoreHints.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EntityFrameworkCoreHints.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OwnersController : ControllerBase
    {
        private readonly IGenericRepository<Owner> _ownersRepository;

        public OwnersController(IGenericRepository<Owner> ownersRepository)
        {
            _ownersRepository = ownersRepository;
        }

        // GET api/owners
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var owners = await _ownersRepository.AllAsync();
            if (owners == null)
                return NotFound();

            return Ok(owners);
        }

        // GET api/owners/1
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var owner = await _ownersRepository.GetAsync(id);
            if (owner == null)
                return NotFound();

            return Ok(owner);
        }

        // POST api/owners
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Owner owner)
        {
            var createdOwner = await _ownersRepository.InsertAsync(owner);

            return Created(new Uri($"api/owners/{createdOwner.Id}", UriKind.Relative), createdOwner);
        }

        // PUT api/owners/1
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Owner owner)
        {
            var existingOwner = await _ownersRepository.GetAsync(id);
            if (existingOwner == null)
                return NotFound();

            owner.Id = id;
            existingOwner = await _ownersRepository.UpdateAsync(owner);

            return NoContent();
        }

        // DELETE api/owners/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            bool deletedOwner = await _ownersRepository.DeleteAsync(id);

            if (!deletedOwner)
                return NotFound();

            return NoContent();
        }
    }
}
