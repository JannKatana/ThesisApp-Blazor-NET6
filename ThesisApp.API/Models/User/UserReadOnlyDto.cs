using System.ComponentModel.DataAnnotations;
using ThesisApp.API.Models.Device;
using ThesisApp.API.Models.DeviceUser;

namespace ThesisApp.API.Models.User
{
    public class UserReadOnlyDto: BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }
    }
}
