using System;
using System.ComponentModel.DataAnnotations;

namespace AP.PD.Domain
{
    public class ParcelOrderDomain
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "RepId")]
        public Guid RepId { get; set; }

        [Required]
        [Display(Name = "TotalQuantity")]
        public int TotalQuantity { get; set; }
    }
}
