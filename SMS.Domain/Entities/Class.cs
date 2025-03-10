using SMS.Domain.Common.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace SMS.Domain.Entities
{
    public class Class : BaseEntity<Guid>
    {
        public string ClassName { get; set; }
        public int RoomNumber { get; set; }
    }
}
