using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Dtos
{
    public class ReservationDto
    {
        public int Id { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        //[Required, Range(1, 10)]
        public int NumberOfPeopel { get; set; }
        [NotMapped]
        public double TotalPrice { get; set; }
        [DefaultValue(1D)]
        public double Discount { get; set; }
        [Required, DefaultValue(ReservationStatus.Booked)]
        public ReservationStatus Status { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
        public string ApplicationUserId { get; set; }
    }
}
