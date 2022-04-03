using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAInnovateChallenge.Models
{
    public class BarPosts
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("Bar")]
        public Guid BarId { get; set; }

        public Bar? Bar { get; set; }

        public DateTime PostDate { get; set; }
        public string Post { get; set; }
    }
}
