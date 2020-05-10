using CakesFactory.Core.Model;
using System;
using System.Threading.Tasks;

namespace CakesFactory.Core.DataInterfaces
{
    public interface ICakeBoxRepository : IRepository<CakeBox>
    {
        Task MarkAsSpecialOffer(Guid cakeBoxId);
        Task<int> GetAvailableBoxesInStock();
    }
}
