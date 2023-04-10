using System.ComponentModel.DataAnnotations;

namespace CVAsp.NetProject.Models
{
    public class RegisterViewModel : LoginViewModel
    {

        [Required(ErrorMessage = "Re-Password is required.")]
        [MinLength(6, ErrorMessage = "Re-Password can be min 6 characters.")]
        [MaxLength(16, ErrorMessage = "Re-Password can be max 16 characters.")]
        [Compare(nameof(Password))]
        public string RePassword { get; set; }

        [Required]
        [StringLength(50)]
        public string FullName { get; set; }

        public bool Locked { get; set; }


        [Required]
        [StringLength(50)]
        public string Role { get; set; } = "user";

        public string? Done { get; set; }
    }
}
