namespace Futsal_Management.Domain.ViewModel
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int UserGroupId { get; set; }
        public string? Token { get; set; }
    }
}
