using System.ComponentModel.DataAnnotations;

namespace ThesisApp.API.Models.User
{
    public class UserUpdateDto: BaseDto
    {
        [Required]
        public string Name { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [Phone]
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
