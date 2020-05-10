using System;
using System.Threading.Tasks;

namespace CakesFactory.Core.Services
{
    public interface ICakeBoxOrderingService
    {
        Task<CakeBoxOrderResult> CreateOrderAsync(Guid customerId, int numberOfCakeBoxesInOrder);
    }
}
