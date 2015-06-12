using System;
using System.ComponentModel.DataAnnotations;

namespace AP.PD.Domain
{
    public class ParcelDeliveryDomain
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "RepUserId")]
        public Guid RepUserId { get; set; }

        [Required]
        [Display(Name = "DeliveredDate")]
        public DateTime DeliveredDate { get; set; }

        [Required]
        [Display(Name = "DeliveredQuantity")]
        public int DeliveredQuantity { get; set; }

        public virtual UserDomain RepUser { get; set; }

        [Required]
        [Display(Name = "CategoryId")]
        public Guid CategoryId { get; set; }

        public virtual CategoryDomain Category { get; set; }
    }
}