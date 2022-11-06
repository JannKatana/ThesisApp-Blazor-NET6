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
            CreateMap<User, UserCreateDto>().ReverseMap();
            CreateMap<User, UserDetailsDto>().ReverseMap();
            CreateMap<User, UserReadOnlyDto>().ReverseMap();
            CreateMap<User, UserUpdateDto>().ReverseMap();

            CreateMap<DeviceUser, AssignedDeviceDto>().ReverseMap();
            CreateMap<DeviceUser, AssignedUsersDto>().ReverseMap();

            CreateMap<Device, DeviceCreateDto>().ReverseMap();
            CreateMap<Device, DeviceDetailsDto>()
                .ForMember(deviceDto => deviceDto.Room, device => device.MapFrom(map => map.Room.Name))
                .ReverseMap();
            CreateMap<Device, DeviceReadOnlyDto>()
                .ForMember(deviceDto => deviceDto.Room, device => device.MapFrom(map => map.Room.Name));
            CreateMap<Device, DeviceUpdateDto>().ReverseMap();

        }
    }
}
