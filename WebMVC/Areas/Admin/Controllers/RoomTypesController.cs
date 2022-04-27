using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Repository.IRepository;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RoomTypesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public RoomTypesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _unitOfWork.RoomType.GetAllAsync(SD.RoomTypePath, Request.Cookies["JWToken"]) });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            RoomType obj = new();

            if (id == null || id == 0)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await _unitOfWork.RoomType.GetFirstOrDefaultAsync(SD.RoomTypePath, id.GetValueOrDefault(), Request.Cookies["JWToken"]);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(RoomType obj)
        {
            if (!ModelState.IsValid) return View(obj);

            if (obj.Id == 0)
            {
                // Create
                await _unitOfWork.RoomType.AddAsync(SD.RoomTypePath, obj, Request.Cookies["JWToken"]);
                TempData["success"] = "RoomType created successfully";
            }
            else
            {
                // update
                await _unitOfWork.RoomType.UpdateAsync(SD.RoomTypePath + obj.Id, obj, Request.Cookies["JWToken"]);
                TempData["success"] = "RoomType updated successfully";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.RoomType.RemoveAsync(SD.RoomTypePath, id,Request.Cookies["JWToken"]);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}
