using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class ReservationVM
    {
        public Reservation Reservation { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoomList { get; set; }
    }
}
