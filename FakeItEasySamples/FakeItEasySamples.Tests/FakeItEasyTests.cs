using System;
using FakeItEasy;
using FakeItEasySamples.Tests.Models;
using FakeItEasySamples.Tests.Models.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FakeItEasySamples.Tests
{
    [TestClass]
    public class FakeItEasyTests
    {
        [TestInitialize]
        public void TestInitialize()
        {

        }

        [TestMethod]
        public void TestMethod1()
        {
            var distributor = A.Fake<Distributor>();
            var car = A.Fake<Car>(x => x.WithArgumentsForConstructor(() => new Car("WB 82112")));
            var petrolStation = A.Fake<PetrolStation>();
            var transaction = A.Fake<Transaction>();
            var vehicle = A.Fake<IVehicle>();

            transaction.getFuelTotalPrice(distributor, 4);

            A.CallTo(() => distributor.getFuelPrice()).MustHaveHappenedOnceExactly();
            A.CallTo(() => transaction.Finish()).DoesNothing();

            A.CallTo(() => vehicle.CheckEngineStatus()).Throws<NotImplementedException>();

            try
            {
                vehicle.CheckEngineStatus();
            }
            catch (Exception ex)
            {
                var exceptionType = ex.GetType();
                Assert.AreEqual(exceptionType, typeof(NotImplementedException));
            }

            distributor.RefuelTheVehicle(A.Dummy<IVehicle>());


            A.CallTo(() => distributor.getFuelPrice()).Returns(4.5M).Once();
            var fuelPrice = distributor.getFuelPrice();

            Assert.AreNotEqual(fuelPrice, 0);
        }
    }
}
