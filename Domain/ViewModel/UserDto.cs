using System.ComponentModel.DataAnnotations;

namespace Futsal_Management.Domain.ViewModel
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }

        public int UserGroupId { get; set; }
    }
}