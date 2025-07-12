using System.ComponentModel.DataAnnotations;

namespace E_exam.DTOs.UserDTOs
{
    public class UserRegisterDTO
    {
        [Required]
        //[StringLength(50, MinimumLength = 5, ErrorMessage = "Must enter a valid email")]
        public string email { get; set; }

        [Required]
        //[StringLength(50, MinimumLength = 8, ErrorMessage = "Password must be at least chars")]
        public string password { get; set; }
    }
}
