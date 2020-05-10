using CakesFactory.Core.DataInterfaces;
using CakesFactory.Core.Model;
using CakesFactory.Core.Model.Enum;
using System;
using System.Threading.Tasks;

namespace CakesFactory.Core.Services
{
    public class CakeBoxOrderingService : ICakeBoxOrderingService
    {
        private readonly ICakeBoxRepository _cakeBoxRepository;
        private readonly ICakeBoxOrderRepository _cakeBoxOrderRepository;

        public CakeBoxOrderingService(ICakeBoxRepository cakeBoxRepository, ICakeBoxOrderRepository cakeBoxOrderRepository)
        {
            _cakeBoxRepository =
              cakeBoxRepository ?? throw new ArgumentNullException(nameof(cakeBoxRepository));
            _cakeBoxOrderRepository =
              cakeBoxOrderRepository ?? throw new ArgumentNullException(nameof(cakeBoxOrderRepository));
        }

        public async Task<CakeBoxOrderResult> CreateOrderAsync(Guid customerId, int numberOfCakeBoxesInOrder)
        {
            if (customerId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(customerId));
            }

            if (numberOfCakeBoxesInOrder < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(numberOfCakeBoxesInOrder),
                  $"{nameof(numberOfCakeBoxesInOrder)} must be greater than zero");
            }

            var availableBoxes = await _cakeBoxRepository.GetAvailableBoxesInStock();

            if (availableBoxes < numberOfCakeBoxesInOrder)
            {
                return new CakeBoxOrderResult
                {
                    ResultCode = CakeBoxOrderCreationResultCode.NoSuccess,
                    ErrorMessage = "Not enough cake boxes available in stock"
                };
            }

            else
            {
                CakeBoxOrder cakeBoxOrder = new CakeBoxOrder
                {
                    Id = Guid.NewGuid(),
                    NumberOfBoxes = numberOfCakeBoxesInOrder,
                    CustomerId = customerId
                };

                await _cakeBoxOrderRepository.SaveAsync(cakeBoxOrder);

                return new CakeBoxOrderResult
                {
                    ResultCode = CakeBoxOrderCreationResultCode.Success,
                    CreatedCakeBoxOrder = cakeBoxOrder
                };
            }
        }

        internal static double CalculateDiscountPercentage(CustomerMembership membership, int numberOfOrderedCakeBoxes)
        {
            var discountInPercent = 0.0;

            if (numberOfOrderedCakeBoxes > 4)
            {
                discountInPercent = 3;
            }

            if (membership == CustomerMembership.Premium)
            {
                discountInPercent += 5;
            }

            return discountInPercent;
        }
    }
}
