using System;
using System.ComponentModel.DataAnnotations;

namespace AP.PD.Domain
{
    public class UserDomain
    {
        [Key]
        [Required]
        [Display(Name = "Id")]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "User name")]
        public string LoginId { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        public Guid RoleId { get; set; }

        public RoleDomain Role { get; set; }

    }
}