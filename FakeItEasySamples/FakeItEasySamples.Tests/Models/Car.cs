using FakeItEasySamples.Tests.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeItEasySamples.Tests.Models
{
    public class Car : IVehicle
    {
        private string _plateNumber;
        public string PlateNumber => _plateNumber;

        public Car(string plateNumber)
        {
            _plateNumber = plateNumber;
        }

        public void CheckEngineStatus()
        {
            Console.WriteLine("Engine OK");
        }

        public void OpenFuelTank()
        {
            Console.WriteLine("Fuel tank opened");
        }
    }
}
