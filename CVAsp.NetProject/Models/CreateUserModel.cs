using System.ComponentModel.DataAnnotations;

namespace CVAsp.NetProject.Models
{
    public class CreateUserModel : RegisterViewModel
    {


    }

    public class EditUserModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(30, ErrorMessage = "Username can be max 30 characters.")]
        public string Username { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public bool Locked { get; set; }


        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";
        public string? Done { get; set; }

        [EmailAddress]
        [StringLength(50)]
        public string? Email { get; set; }

        [Phone]
        [StringLength(25)]
        public string? PhoneNumber { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }

        [Url]
        [StringLength(150)]
        public string? Github { get; set; }

        [Url]
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

        public IFormFile? File { get; set; }

        [StringLength(255)]
        public string ProfileImageFileName { get; set; } = "no-image.png";
    }

}
