using ThesisApp.API.Models.Device;
using ThesisApp.API.Models.DeviceUser;

namespace ThesisApp.API.Models.User
{
    public class UserDetailsDto: UserReadOnlyDto
    {
        public virtual IList<AssignedDeviceDto> AssignedDevices { get; set; }
    }
}
