using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class CreateRoomDto
    {
        [Required, Range(0, 10000)]
        public int? RoomNumber { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomFloor { get; set; }
        [DefaultValue(1.0), Range(1D, 5D)]
        public double? PriceFactor { get; set; }
        [Required,Display(Name ="Room Type")]
        public int? RoomTypeId { get; set; }
        [Required, Display(Name = "Branch")]
        public int? BranchId { get; set; }
    }
}
