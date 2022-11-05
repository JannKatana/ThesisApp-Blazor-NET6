using Microsoft.EntityFrameworkCore;
using ThesisApp.API.Data;
namespace ThesisApp.API.Data
{
    public class ThesisAppDbContext : DbContext
    {
        protected ThesisAppDbContext()
        {
        }

        public ThesisAppDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Device> Devices { get; set; }
        public virtual DbSet<DeviceUser> DeviceUsers { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(m => m.AssignedDevices)
                .WithOne(m => m.User)
                .HasForeignKey(m => m.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Device>().HasData(
                 new Device
                 {
                     Id = 1,
                     Name = "Device01",
                     IsActive = false,
                     RoomId = 1,
                 },
                 new Device
                 {
                     Id = 2,
                     Name = "Device02",
                     IsActive = false,
                     RoomId = 1,
                 },
                 new Device
                 {
                     Id = 3,
                     Name = "Device03",
                     IsActive = false,
                     RoomId = 2,
                 },
                  new Device
                  {
                      Id = 4,
                      Name = "Device04",
                      IsActive = false,
                      RoomId = 2,
                  }

             );

            modelBuilder.Entity<Room>().HasData(
               new Room
               {
                   Id = 1,
                   Name = "Room01",
               },
               new Room
               {
                   Id = 2,
                   Name = "Room02",
               }
           );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Mel-john Catanaoan",
                    Email = "mjtcatanaoan@test.com",
                    PhoneNumber = "+44 (452) 886 09 12",
                }
            );

            modelBuilder.Entity<DeviceUser>().HasData(
                new DeviceUser
                {
                    Id = 1,
                    DeviceId = 1,
                    UserId = 1
                }
            );
        }
    }
}