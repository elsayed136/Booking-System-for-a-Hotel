using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Dtos
{
    public class CreateReservationDto
    {
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        //[Range(1, 5)]
        public int NumberOfPeopel { get; set; }
        [NotMapped]
        public double TotalPrice { get; set; }
        [DefaultValue(0D)]
        public double Discount { get; set; }
        [DefaultValue(ReservationStatus.Booked)]
        public ReservationStatus Status { get; set; }
        [Required,Display(Name ="Room")]
        public int RoomId { get; set; }
        [Required, Display(Name = "User")]
        public string ApplicationUserId { get; set; }
    }
}
