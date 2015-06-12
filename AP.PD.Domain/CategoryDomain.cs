using System;
using System.ComponentModel.DataAnnotations;

namespace AP.PD.Domain
{
    public class CategoryDomain
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Description { get; set; }
    }
}
