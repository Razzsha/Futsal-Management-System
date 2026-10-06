namespace Futsal_Management.Domain.Model
{
    public class UserGroup : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
