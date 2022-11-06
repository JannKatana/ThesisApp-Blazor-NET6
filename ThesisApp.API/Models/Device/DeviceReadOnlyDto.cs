using ThesisApp.API.Data;
using ThesisApp.API.Models.DeviceUser;
using ThesisApp.API.Models.Room;
using ThesisApp.API.Models.User;

namespace ThesisApp.API.Models.Device
{
    public class DeviceReadOnlyDto: BaseDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public string Room { get; set; }
    }
}
