using ThesisApp.API.Models.DeviceUser;

namespace ThesisApp.API.Models.Device
{
    public class DeviceDetailsDto: DeviceReadOnlyDto
    {
        public virtual IList<AssignedUsersDto> AssignedUsers { get; set; }
    }
}
