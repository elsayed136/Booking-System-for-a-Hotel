using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebMVC.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        [Required, DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [Required, DataType(DataType.Date)]
        public DateTime EndDate { get; set; }= DateTime.Now.AddDays(1);
        [Required, DefaultValue(1), Range(1, 5)]
        public int NumberOfPeopel { get; set; } = 1;
        [Column(TypeName = "money")]
        public double TotalPrice { get; set; }
        [DefaultValue(0D)]
        public double Discount { get; set; }
        [DefaultValue(ReservationStatus.Booked)]
        public ReservationStatus Status { get; set; }
        [Required,Display(Name ="Room")]
        public int RoomId { get; set; }
        [ValidateNever]
        public Room Room { get; set; }
        public string ApplicationUserId { get; set; }
    }
    public enum ReservationStatus { Booked, Canceled }
}
