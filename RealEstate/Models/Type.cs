using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Type
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        public ICollection<Estate>? Estates { get; set; }
    }
}
