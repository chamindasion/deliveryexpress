using System;
using System.ComponentModel.DataAnnotations;

namespace AP.PD.Domain
{
    public class RoleDomain
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
