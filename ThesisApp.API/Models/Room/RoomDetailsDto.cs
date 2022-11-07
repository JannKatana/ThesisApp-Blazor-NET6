using ThesisApp.API.Models.Device;

namespace ThesisApp.API.Models.Room
{
    public class RoomDetailsDto: RoomReadOnlyDto
    {
        public IList<DeviceReadOnlyDto> InstalledDevices { get; set; }
    }
}
