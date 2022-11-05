using System.ComponentModel.DataAnnotations.Schema;

namespace ThesisApp.API.Data
{
    public class DeviceUser
    {
        public int Id { get; set; }
        
        [ForeignKey(nameof(DeviceId))]
        public int DeviceId { get; set; }
        public virtual Device Device { get; set; }

        [ForeignKey(nameof(UserId))]
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
