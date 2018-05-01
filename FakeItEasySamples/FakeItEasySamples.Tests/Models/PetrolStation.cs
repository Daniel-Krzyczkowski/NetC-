using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeItEasySamples.Tests.Models
{
    public class PetrolStation
    {
        public Distributor GetDistributorForVehicle(string plateNumber)
        {
            return new Distributor();
        }
    }
}
