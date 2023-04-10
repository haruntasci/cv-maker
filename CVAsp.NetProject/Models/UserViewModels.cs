﻿using System.ComponentModel.DataAnnotations;

namespace CVAsp.NetProject.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string? FullName { get; set; }
        public string Username { get; set; }
        public bool Locked { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string ProfileImageFileName { get; set; } = "no-image.png";
        public string Role { get; set; } = "user";
    }
}
