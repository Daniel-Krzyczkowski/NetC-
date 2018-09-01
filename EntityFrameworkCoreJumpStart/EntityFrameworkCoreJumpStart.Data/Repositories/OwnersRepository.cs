using EntityFrameworkCoreJumpStart.Data.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkCoreJumpStart.Data.Repositories
{
    public class OwnersRepository : IGenericRepository<Owner>
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OwnersRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IEnumerable<Owner>> AllAsync()
        {
            var owners = await _applicationDbContext.Owners
               .ToListAsync();

            return owners;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var owner = await _applicationDbContext.Owners
           .Where(x => x.Id == id)
           .SingleOrDefaultAsync();

            if (owner == null)
                return false;

            _applicationDbContext.Owners.Remove(owner);
            await _applicationDbContext.SaveChangesAsync();

            return true;
        }

        public async Task<Owner> GetAsync(Guid id)
        {
            return await _applicationDbContext.Owners
               .Where(x => x.Id == id)
               .SingleOrDefaultAsync();
        }

        public async Task<Owner> InsertAsync(Owner owner)
        {
            owner.Id = Guid.NewGuid();

            await _applicationDbContext.Owners.AddAsync(owner);
            await _applicationDbContext.SaveChangesAsync(true);

            return owner;
        }

        public async Task<Owner> UpdateAsync(Owner owner)
        {
            var existingOwner = await _applicationDbContext.Owners
               .Where(x => x.Id == owner.Id)
               .SingleOrDefaultAsync();

            if (existingOwner == null)
                return null;

            existingOwner.FirstName = owner.FirstName;
            existingOwner.LastName = owner.LastName;
            existingOwner.PhoneNumber = owner.PhoneNumber;

            await _applicationDbContext.SaveChangesAsync(true);

            return existingOwner;
        }
    }
}
