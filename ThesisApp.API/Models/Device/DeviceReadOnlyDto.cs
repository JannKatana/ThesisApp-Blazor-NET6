using ThesisApp.API.Data;
using ThesisApp.API.Models.Room;

namespace ThesisApp.API.Models.Device
{
    public class DeviceReadOnlyDto: BaseDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public RoomLocationDto RoomLocation { get; set; }
    }
}
