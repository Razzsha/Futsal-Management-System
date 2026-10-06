using Futsal_Management.Domain.Model;
using Futsal_Management.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Futsal_Management.Domain.ViewModel
{
    public class BookingInfoDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "User is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid UserId")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Arena is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid ArenaId")]
        public int ArenaId { get; set; }

        [Required(ErrorMessage = "Request date is required")]
        public DateTime RequestDate { get; set; }

        //[Required(ErrorMessage = "Request time is required")]
        public TimeSpan? RequestTime { get; set; }

        public BookingStatus Status { get; set; } = BookingStatus.New;

        [Required(ErrorMessage = "Full name is required")]
        [StringLength(100)]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Contact number is required")]
        [StringLength(20)]
        public string? ContactNo { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string? Email { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Total cost cannot be negative")]
        public decimal TotalCost { get; set; }

        [StringLength(500)]
        public string? Remarks { get; set; }
    }
}