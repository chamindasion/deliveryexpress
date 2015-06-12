using System;

namespace AP.PD.Shared
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string LoginId { get; set; }

        public string Password { get; set; }

        public Guid RoleId { get; set; }

        public RoleDto Role { get; set; }
    }
}