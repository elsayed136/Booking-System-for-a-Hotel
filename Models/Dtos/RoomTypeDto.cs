using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class RoomTypeDto
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required,Range(1D, 1000D)]
        public double? Price { get; set; }
        public int MaxClientsAllowed { get; set; }
    }
}
