using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisApp.API.Models.Device
{
    public class DeviceUpdateDto: BaseDto
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int? RoomId { get; set; }
    }
}
