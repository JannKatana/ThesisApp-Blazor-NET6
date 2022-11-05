using ThesisApp.API.Models.Device;

namespace ThesisApp.API.Models.DeviceUser
{
    public class AssignedDeviceDto
    {
        public virtual DeviceReadOnlyDto Device { get; set; }
    }
}
