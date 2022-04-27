using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Repository.IRepository;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BranchesController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchesController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _unitOfWork.Branch.GetAllAsync(SD.BranchPath, Request.Cookies["JWToken"]) });
        }
        public async Task<IActionResult> Upsert(int? id)
        {
            Branch obj = new();

            if (id == null || id == 0)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj = await _unitOfWork.Branch.GetFirstOrDefaultAsync(SD.BranchPath, id.GetValueOrDefault(), Request.Cookies["JWToken"]);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(Branch obj)
        {
            if (!ModelState.IsValid) return View(obj);

            if (obj.Id == 0)
            {
                // Create
                await _unitOfWork.Branch.AddAsync(SD.BranchPath, obj, Request.Cookies["JWToken"]);
                TempData["success"] = "Branch created successfully";
            }
            else
            {
                // update
                await _unitOfWork.Branch.UpdateAsync(SD.BranchPath + obj.Id, obj, Request.Cookies["JWToken"]);
                TempData["success"] = "Branch updated successfully";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.Branch.RemoveAsync(SD.BranchPath, id, Request.Cookies["JWToken"]);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
    }
}
