using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using WebMVC.Models;
using WebMVC.Repository.IRepository;
using WebMVC.ViewModels;

namespace WebMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ReservationsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _unitOfWork.Reservation.GetAllAsync(SD.ReservavtionPath, Request.Cookies["JWToken"]) });
        }
        public async Task<IActionResult> GetAllByUserId()
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(Request.Cookies["JWToken"]);
            var uid = jwtSecurityToken.Claims.First(claim => claim.Type == "uid").Value;
            return View(await _unitOfWork.Reservation.GetAllAsync(SD.ReservavtionPath + "GetByUserId/" + uid, Request.Cookies["JWToken"]));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(int? id)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(Request.Cookies["JWToken"]);
            var uid = jwtSecurityToken.Claims.First(claim => claim.Type == "uid").Value;
            IEnumerable<Room> roomList = await _unitOfWork.Room.GetAllAsync(SD.RoomPath, Request.Cookies["JWToken"]);
            ReservationVM obj = new()
            {
                Reservation = new Reservation() { ApplicationUserId = uid },

                RoomList = roomList.Select(i => new SelectListItem
                {
                    Text = i.RoomNumber.ToString(),
                    Value = i.Id.ToString()
                }),
            };

            if (id == null || id == 0)
            {
                //this will be true for Insert/Create
                return View(obj);
            }

            //Flow will come here for update
            obj.Reservation = await _unitOfWork.Reservation.GetFirstOrDefaultAsync(SD.ReservavtionPath, id.GetValueOrDefault(), Request.Cookies["JWToken"]);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Upsert(ReservationVM obj)
        {

            if (!ModelState.IsValid) return View(obj);

            if (obj.Reservation.Id == 0)
            {
                // Create

                await _unitOfWork.Reservation.AddAsync(SD.ReservavtionPath, obj.Reservation, Request.Cookies["JWToken"]);
                TempData["success"] = "Branch created successfully";
            }
            else
            {
                // update
                await _unitOfWork.Reservation.UpdateAsync(SD.ReservavtionPath + obj.Reservation.Id, obj.Reservation, Request.Cookies["JWToken"]);
                TempData["success"] = "Branch updated successfully";
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var status = await _unitOfWork.Reservation.RemoveAsync(SD.ReservavtionPath, id, Request.Cookies["JWToken"]);
            if (status)
            {
                return Json(new { success = true, message = "Delete Successful" });
            }
            return Json(new { success = false, message = "Delete Not Successful" });
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Create(int roomId)
        {
            var jwtSecurityToken = new JwtSecurityTokenHandler().ReadJwtToken(Request.Cookies["JWToken"]);
            var uid = jwtSecurityToken.Claims.First(claim => claim.Type == "uid").Value;

            var room = await _unitOfWork.Room.GetFirstOrDefaultAsync(SD.RoomPath, roomId, Request.Cookies["JWToken"]);

            return View(new Reservation()
            {
                RoomId = roomId,
                ApplicationUserId = uid,
                Room = room,
            });
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Reservation obj)
        {
            if (!ModelState.IsValid) return View(obj);

            await _unitOfWork.Reservation.AddAsync(SD.ReservavtionPath, obj, Request.Cookies["JWToken"]);

            TempData["success"] = "Branch created successfully";

            return RedirectToAction(nameof(GetAllByUserId));
        }

    }
}
