using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomNumber { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomFloor { get; set; }

        [DefaultValue(1.0),Range(1D,5D)]
        public double? PriceFactor { get; set; }

        public int RoomTypeId { get; set; }
        [ForeignKey("RoomTypeId")]
        public RoomType RoomType { get; set; }
        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; }

    }
}
