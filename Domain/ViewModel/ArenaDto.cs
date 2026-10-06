using System.ComponentModel.DataAnnotations;

namespace Futsal_Management.Domain.ViewModel
{
    public class ArenaDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Arena name is required")]
        [StringLength(100, ErrorMessage = "Arena name cannot exceed 100 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Area is required")]
        [StringLength(200, ErrorMessage = "Area cannot exceed 200 characters")]
        public string? Area { get; set; }

        [Range(1, double.MaxValue, ErrorMessage = "Hourly rate must be greater than 0")]
        public decimal Hour { get; set; }
    }
}