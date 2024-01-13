using System.ComponentModel.DataAnnotations;

namespace RealEstate.Models
{
    public class Estate
    {
        public int Id { get; set; }

        [MinLength(3)]
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }


        [Required]
        public int BedRooms { get; set; }

        [Required]
        public int BathRooms { get; set; }

        [Required]
        public int SquareFeet { get; set; }

        public int? TypeId { get; set; }

        public Type? Type { get; set; }


    }
}
