namespace Domain.Entities.Common
{
    public abstract class BaseEntity : IBaseEntity<string>
    {
        public string Id { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
