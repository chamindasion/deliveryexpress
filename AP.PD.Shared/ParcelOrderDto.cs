using System;

namespace AP.PD.Shared
{
    public class ParcelOrderDto
    {
        public Guid Id { get; set; }

        public DateTime OrderDate { get; set; }

        public Guid RepId { get; set; }

        public int TotalQuantity { get; set; }
    }
}
