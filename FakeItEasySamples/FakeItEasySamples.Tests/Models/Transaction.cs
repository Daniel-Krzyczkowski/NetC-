using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeItEasySamples.Tests.Models
{
    public class Transaction
    {
        private bool _isCompleted;
        public decimal getFuelTotalPrice(Distributor distributor, int liters)
        {
            return liters * distributor.getFuelPrice();
        }

        public virtual void Finish()
        {
            _isCompleted = true;
        }
    }
}
