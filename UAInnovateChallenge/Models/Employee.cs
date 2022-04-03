using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAInnovateChallenge.Models
{
    public class Employee
    {
        [Key]
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User? User { get; set; }
        [Required]
        [ForeignKey("Bar")]
        public Guid BarId { get; set; }

        public Bar? Bar { get; set; }
    }
}
