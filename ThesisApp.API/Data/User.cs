﻿namespace ThesisApp.API.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string ImageUrl { get; set; }

        public IList<DeviceUser> AssignedDevices { get; set; }
    }
}
