using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVC.Models
{
    public class Room
    {
        public int Id { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomNumber { get; set; }
        [Required, Range(0, 10000)]
        public int? RoomFloor { get; set; }

        [Required,DefaultValue(1.0),Range(1D,5D)]
        public double? PriceFactor { get; set; }
        [Display(Name = "RoomType")]
        public int RoomTypeId { get; set; }
        [ValidateNever]
        public RoomType RoomType { get; set; }
        [Display(Name = "Branch")]
        public int BranchId { get; set; }
        [ValidateNever]
        public Branch Branch { get; set; }
        [NotMapped]
        public bool IsAvailable { get; set; }

    }
}
