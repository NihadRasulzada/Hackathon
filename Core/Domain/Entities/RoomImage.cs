using Domain.Entities.Common;

namespace Domain.Entities
{
    public class RoomImage : BaseEntity
    {
        public string FileName { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
