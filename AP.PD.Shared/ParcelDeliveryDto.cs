using System;

namespace AP.PD.Shared
{
    public class ParcelDeliveryDto
    {
        public Guid Id { get; set; }

        public Guid RepUserId { get; set; }

        public DateTime DeliveredDate { get; set; }

        public int DeliveredQuantity { get; set; }

        public UserDto RepUser { get; set; }

        public Guid CategoryId { get; set; }

        public CategoryDto Category { get; set; }
    }
}