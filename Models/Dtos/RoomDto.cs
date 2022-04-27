using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Dtos
{
    public class RoomDto
    {
        public int Id { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomNumber { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomFloor { get; set; }
        [DefaultValue(1.0), Range(1D, 5D)]
        public double? PriceFactor { get; set; }
        public int RoomTypeId { get; set; }
        public RoomType RoomType { get; set; }
        public int BranchId { get; set; }
        public Branch Branch { get; set; }
    }
}
