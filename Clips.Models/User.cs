﻿namespace Clips.Models
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Password { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;

        public long RoleId { get; set; } = 1;
        public Role? Role { get; set; }
    }
}
