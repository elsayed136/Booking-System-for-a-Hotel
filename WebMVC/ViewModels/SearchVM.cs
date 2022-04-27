using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class SearchVM
    {
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime StartDate { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]
        [BindProperty]
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(1);
        public IEnumerable<Room> RoomList { get; set; } = new List<Room>();
    }
}
