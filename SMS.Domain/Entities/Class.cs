using SMS.Domain.Common.Base;

namespace SMS.Domain.Entities
{
    public class Class : BaseEntity<Guid>
    {
        public string ClassName { get; set; }
    }
}
