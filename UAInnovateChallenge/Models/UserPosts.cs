using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAInnovateChallenge.Models
{
    public class UserPosts
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        public DateTime PostDate { get; set; }
        public string Post { get; set; }
    }
}
