using ThesisApp.API.Models.Device;
using ThesisApp.API.Models.User;

namespace ThesisApp.API.Models.DeviceUser
{
    public class AssignedUsersDto
    {
        public virtual UserReadOnlyDto User { get; set; }

    }
}
