using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVAsp.NetProject.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [StringLength(50)]
        public string? FullName { get; set; }

        [Required]
        [StringLength(30)]
        public string Username { get; set; }

        [Required]
        [StringLength(100)]
        public string Password { get; set; }

        public bool Locked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [StringLength(255)]
        public string ProfileImageFileName { get; set; } = "no-image.png";

        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(25)]
        public string? PhoneNumber { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [StringLength(150)]
        public string? Github { get; set; }

        [StringLength(150)]
        public string? Linkedin { get; set; }

        [StringLength(100)]
        public string? Profession { get; set; }

        [StringLength(300)]
        public string? Languages { get; set; }

        [StringLength(300)]
        public string? Libraries { get; set; }

        [StringLength(300)]
        public string? Platform { get; set; }

        [StringLength(300)]
        public string? Versioning { get; set; }

        public string? Summary { get; set; }



    }

}
