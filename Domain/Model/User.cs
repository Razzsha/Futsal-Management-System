namespace Futsal_Management.Domain.Model
{
    public class User : BaseEntity
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int UserGroupId { get; set; }
        public UserGroup? UserGroup { get; set; }

    }
}
