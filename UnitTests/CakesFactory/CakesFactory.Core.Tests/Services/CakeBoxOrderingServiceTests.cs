using AutoFixture;
using CakesFactory.Core.DataInterfaces;
using CakesFactory.Core.Model;
using CakesFactory.Core.Model.Enum;
using CakesFactory.Core.Services;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace CakesFactory.Core.Tests.Services
{
    public class CakeBoxOrderingServiceTests
    {
        private readonly Fixture _fixture;
        private readonly CakeBoxOrderingService _cakeBoxOrderingService;

        public CakeBoxOrderingServiceTests()
        {
            _fixture = new Fixture();

            var cakeBoxRepositoryMock = new Mock<ICakeBoxRepository>();
            cakeBoxRepositoryMock.Setup(x => x.GetAvailableBoxesInStock())
              .ReturnsAsync(5);

            var ocakeBoxOrderRepositoryMock = new Mock<ICakeBoxOrderRepository>();
            ocakeBoxOrderRepositoryMock.Setup(x => x.SaveAsync(It.IsAny<CakeBoxOrder>()))
              .ReturnsAsync((CakeBoxOrder x) => x);

            _cakeBoxOrderingService = new CakeBoxOrderingService(cakeBoxRepositoryMock.Object, ocakeBoxOrderRepositoryMock.Object);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldStoreCreatedCakeBoxOrderInOrderCreationResult()
        {
            var numberOfOrderedCakeBoxes = 4;
            var customerId = Guid.NewGuid();

            var orderCreationResult =
              await _cakeBoxOrderingService.CreateOrderAsync(customerId, numberOfOrderedCakeBoxes);

            Assert.Equal(CakeBoxOrderCreationResultCode.Success, orderCreationResult.ResultCode);
            Assert.NotNull(orderCreationResult.CreatedCakeBoxOrder);
            Assert.Equal(customerId, orderCreationResult.CreatedCakeBoxOrder.CustomerId);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldReturnStockExceededResult_IfNotEnoughCakeBoxesInStock()
        {
            var numberOfOrderedCakeBoxes = 6;
            var customerId = Guid.NewGuid();

            var orderCreationResult =
              await _cakeBoxOrderingService.CreateOrderAsync(customerId, numberOfOrderedCakeBoxes);

            Assert.Equal(CakeBoxOrderCreationResultCode.NoSuccess, orderCreationResult.ResultCode);
            Assert.Equal("Not enough cake boxes available in stock", orderCreationResult.ErrorMessage);
        }

        [Fact]
        public async Task CreateOrderAsync_ShouldThrowException_IfCustomerIsNull()
        {
            var numberOfOrderedCups = 1;
            var customerId = Guid.Empty;

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(
              () => _cakeBoxOrderingService.CreateOrderAsync(customerId, numberOfOrderedCups));

            Assert.Equal("customerId", exception.ParamName);
        }

        [Theory]
        [InlineData(3, 5, CustomerMembership.Basic)]
        [InlineData(0, 4, CustomerMembership.Basic)]
        [InlineData(8, 5, CustomerMembership.Premium)]
        [InlineData(5, 1, CustomerMembership.Premium)]
        public void ShouldCalculateCorrectDiscountPercentage(double expectedDiscountInPercent, int numberOfOrderedCups,
                                                             CustomerMembership customerMembership)
        {
            var discountInPercent =
              CakeBoxOrderingService.CalculateDiscountPercentage(customerMembership,
                numberOfOrderedCups);

            Assert.Equal(expectedDiscountInPercent, discountInPercent);
        }
    }
}
