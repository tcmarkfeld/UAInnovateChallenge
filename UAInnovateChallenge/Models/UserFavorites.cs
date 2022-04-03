using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAInnovateChallenge.Models
{
    public class UserFavorites
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User? User { get; set; }
        [Required]
        [ForeignKey("Bar")]
        public Guid BarId { get; set; }

        public Bar? Bar { get; set; }

        public bool? IsFavorite { get; set; }
    }
}
