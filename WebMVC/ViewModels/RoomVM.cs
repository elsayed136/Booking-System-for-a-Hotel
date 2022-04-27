using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;

namespace WebMVC.ViewModels
{
    public class RoomVM
    {
        public Room Room { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> BranchList { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> RoomTypeList { get; set; }
    }
}
