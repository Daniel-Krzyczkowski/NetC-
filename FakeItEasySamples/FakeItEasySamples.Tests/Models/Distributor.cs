using FakeItEasySamples.Tests.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeItEasySamples.Tests.Models
{
    public class Distributor
    {
        public virtual decimal getFuelPrice()
        {
            return 4.50M;
        }

        public void RefuelTheVehicle(IVehicle vehicle)
        {
            vehicle.OpenFuelTank();
        }
    }
}
