using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVC.Models
{
    public class RoomType
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, Column(TypeName = "money"), Range(1D, 1000D)]
        // price per night
        public double? Price { get; set; }
        [Required]
        public int? MaxClientsAllowed { get; set; }
    }
}
