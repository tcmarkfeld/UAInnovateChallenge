using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UAInnovateChallenge.Models
{
    public class Bar
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Cover { get; set; }
        public string Bio { get; set; }
        public double WaitTime { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int ZipCode { get; set; }
        public string State { get; set; }
        [DataType(DataType.Upload)]
        [DisplayName("BarPicture")]
        public byte[]? BarPicture { get; set; }
    }
}
