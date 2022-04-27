using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebMVC.Models;
using WebMVC.Repository.IRepository;
using WebMVC.ViewModels;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _unitOfWork.Room.GetAllAsync(SD.RoomPath, Request.Cookies["JWToken"]) });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            IEnumerable<Branch> branchList = await _unitOfWork.Branch.GetAllAsync(SD.BranchPath, Request.Cookies["JWToken"]);
            IEnumerable<RoomType> roomTypeList = await _unitOfWork.RoomType.GetAllAsync(SD.RoomTypePath, Request.Cookies["JWToken"]);
            RoomVM obj = new()
            {
                Room = new Room(),
                BranchList = branchList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),
                RoomTypeList = roomTypeList.Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }),

            };

            if (id == null || id == 0)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj.Room = await _unitOfWork.Room.GetFirstOrDefaultAsync(SD.RoomPath, id.GetValueOrDefault(), Request.Cookies["JWToken"]);
            if (obj.Room == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(RoomVM obj)
        {
            if (!ModelState.IsValid) return View(obj);

            if (obj.Room.Id == 0)
            {
                // Create
                await _unitOfWork.Room.AddAsync(SD.RoomPath, obj.Room, Request.Cookies["JWToken"]);
                TempData["success"] = "Room created successfully";
            }
            else
            {
                // update
                await _unitOfWork.Room.UpdateAsync(SD.RoomPath + obj.Room.Id, obj.Room, Request.Cookies["JWToken"]);
                TempData["success"] = "Room updated successfully";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.Room.RemoveAsync(SD.RoomPath, id, Request.Cookies["JWToken"]);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
        public async Task<IActionResult> RepostPage()
        {
            return View(await _unitOfWork.Room.ReportLike(SD.RoomPath , Request.Cookies["JWToken"]));
        }
    }
}
