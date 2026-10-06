using Futsal_Management.Domain.Enum;
using System.ComponentModel.DataAnnotations;

namespace Futsal_Management.Domain.Model
{
    public class BookingInfo : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ArenaId { get; set; }
        public DateTime RequestDate { get; set; }
        public TimeSpan? RequestTime { get; set; }
        public BookingStatus Status { get; set; }
        public string? FullName { get; set; }
        public string? ContactNo { get; set; }
        public string? Email { get; set; }
        public decimal TotalCost { get; set; }
        public string? Remarks { get; set; }
        public User? User { get; set; }
        public Arena? Arena { get; set; }
    }
}
