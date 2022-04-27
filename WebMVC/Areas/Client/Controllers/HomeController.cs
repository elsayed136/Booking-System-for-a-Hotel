using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using WebMVC.Models;
using WebMVC.Repository.IRepository;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> SearchRoom()
        {
            return View(new SearchVM());
        }
        [HttpPost]
        public async Task<IActionResult> SearchRoom(DateTime startDate, DateTime endDate)
        {
            var rooms = await _unitOfWork.Room.GetAvailableRooms(SD.RoomPath, startDate, endDate);
            var output = new SearchVM
            {
                StartDate = startDate,
                EndDate = endDate,
                RoomList = rooms
            };
            return View(output);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View(new Reservation());
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(Reservation obj)
        {
            if (!ModelState.IsValid) return View(obj);
            var tokenReader = new JwtSecurityTokenHandler().ReadJwtToken(Request.Cookies["JWToken"]);
            var uid = tokenReader.Claims.First(claim => claim.Type == "uid").Value;
            obj.ApplicationUserId = uid;

            await _unitOfWork.Reservation.AddAsync(SD.ReservavtionPath, obj, Request.Cookies["JWToken"]);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}