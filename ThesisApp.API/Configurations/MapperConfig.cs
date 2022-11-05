using AutoMapper;
using ThesisApp.API.Data;
using ThesisApp.API.Models.Device;
using ThesisApp.API.Models.DeviceUser;
using ThesisApp.API.Models.Room;
using ThesisApp.API.Models.User;

namespace ThesisApp.API.Configurations
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<UserReadOnlyDto, User>().ReverseMap();
            CreateMap<UserUpdateDto, User>().ReverseMap();

            CreateMap<DeviceReadOnlyDto, Device>().ReverseMap();
            CreateMap<AssignedDeviceDto, DeviceUser>().ReverseMap();
            
            CreateMap<RoomLocationDto, Room>().ReverseMap();
        }
    }
}
