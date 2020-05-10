using System;

namespace CakesFactory.Core.Model
{
    public class CakeBoxOrder : BaseEntity
    {
        public int NumberOfBoxes { get; set; }
        public Guid CustomerId { get; set; }
    }
}
