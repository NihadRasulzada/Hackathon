using Domain.Enums;

namespace Domain.Entities
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public string? EntityName { get; set; }
        public string? EntityId { get; set; }
        public string? PropertyName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public AuditAction? ChangeType { get; set; }
        public DateTime? ChangeDate { get; set; }
        public string? UserId { get; set; }
    }
}
