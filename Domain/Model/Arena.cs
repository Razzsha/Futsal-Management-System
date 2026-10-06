using System.ComponentModel.DataAnnotations;

namespace Futsal_Management.Domain.Model
{
    public class Arena : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Area { get; set; }
        public decimal Hour { get; set; }
    }
}
