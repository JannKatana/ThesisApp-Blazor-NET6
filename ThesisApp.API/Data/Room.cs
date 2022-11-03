namespace ThesisApp.API.Data
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<User> Users { get; set; }
        public IList<Device> Devices { get; set; }

    }
}
