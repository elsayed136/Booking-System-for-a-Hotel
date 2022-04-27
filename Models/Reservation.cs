using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    public class Reservation
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [DefaultValue(1), Range(1, 5)]
        public int NumberOfPeopel { get; set; }
        [Column(TypeName = "money")]
        public double TotalPrice { get; set; }
        [DefaultValue(0D)]
        public double Discount { get; set; }
        [Required,DefaultValue(ReservationStatus.Booked)]
        public ReservationStatus Status { get; set; }
        public int RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }
    }
    public enum ReservationStatus { Booked, Canceled }
}
