using System.ComponentModel.DataAnnotations;

namespace MyBlog.Models
{
    public class PasswordChangeModel
    {

        [Required]
        [DataType(DataType.Password)]
        public string? NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? OldPassword { get; set; }

        [Compare("NewPassword", ErrorMessage = "The password and confirmation do not match")]
        public string? ConfirmPassword { get; set; }


    }
}
