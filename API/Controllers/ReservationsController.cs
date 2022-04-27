using AutoMapper;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Dtos;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ReservationsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReservationsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            var reservation = _unitOfWork.Reservation.GetAll(null, new string[] { $"{nameof(Room)}" });
            var reservationDto = new List<ReservationDto>();
            foreach (var item in reservation)
            {
                reservationDto.Add(_mapper.Map<ReservationDto>(item));
            }
            return Ok(reservationDto);
        }
        [HttpGet("GetByUserId/{userId}", Name = "GetByUserId")]
        [AllowAnonymous]
        public IActionResult GetAllByUserId(string userId)
        {
            var reservation = _unitOfWork.Reservation.GetAll(r => r.ApplicationUserId == userId, new string[] { $"{nameof(Room)}" });
            var reservationDto = new List<ReservationDto>();
            foreach (var item in reservation)
            {
                reservationDto.Add(_mapper.Map<ReservationDto>(item));
            }
            return Ok(reservationDto);
        }

        [HttpGet("{reservationId}", Name = "GetReservationById")]
        [AllowAnonymous]
        public IActionResult GetById(int reservationId)
        {
            var obj = _unitOfWork.Reservation.GetFirstOrDefault(b => b.Id == reservationId,
                    new string[] { $"{nameof(Room)}" }
                );
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<ReservationDto>(obj);

            return Ok(objDto);
        }
        [HttpPost]
        [Authorize]
        public IActionResult Create(CreateReservationDto dto)
        {
            if (dto == null) return BadRequest(ModelState);

            var hasReserveredBefore = _unitOfWork.Reservation
                .Any(res => res.ApplicationUserId == dto.ApplicationUserId);
            if (hasReserveredBefore)
            {
                dto.Discount = 0.95;
            }
            // find room by id 
            var room = _unitOfWork.Room.GetFirstOrDefault(room => room.Id == dto.RoomId,
                    new string[] { $"{nameof(RoomType)}" }
                );

            // room price per night
            double roomPrice = (double)(room.PriceFactor * room.RoomType.Price);
            //
            double daysCount = (dto.EndDate - dto.StartDate).TotalDays;
            //
            dto.TotalPrice = daysCount * roomPrice * (1 - dto.Discount);
            //
            if (room.RoomType.Name == "single".ToLower())
            {
                dto.NumberOfPeopel = 1;
            }

            var reservation = _mapper.Map<Reservation>(dto);

            _unitOfWork.Reservation.Add(reservation);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {reservation.Id}");
                return StatusCode(500, ModelState);
            }
            // return Ok(dto);
            return CreatedAtRoute("GetReservationById", new { reservationId = reservation.Id }, reservation);
        }

        [HttpPatch("{reservationId:int}")]
        [Authorize]
        public IActionResult Update(int reservationId, UpdateReservationDto dto)
        {
            if (dto == null || reservationId != dto.Id) return BadRequest(ModelState);

            var reservation = _mapper.Map<Reservation>(dto);

            var hasReserveredBefore = _unitOfWork.Reservation
                .Any(res => res.ApplicationUserId == reservation.ApplicationUserId);
            if (hasReserveredBefore)
            {
                reservation.Discount = 0.95;
            }
            // find room by id 
            var room = _unitOfWork.Room.GetFirstOrDefault(room => room.Id == reservation.RoomId,
                    new string[] { $"{nameof(RoomType)}" }
                );
            // room price per night
            double roomPrice = (double)(room.PriceFactor * room.RoomType.Price);
            //
            var daysCount = (reservation.EndDate - reservation.StartDate).TotalDays;
            //
            reservation.TotalPrice = daysCount * roomPrice * (1 - reservation.Discount);

            _unitOfWork.Reservation.Update(reservation);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {reservation.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{reservationId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int reservationId)
        {
            var reservation = _unitOfWork.Reservation.GetFirstOrDefault(b => b.Id == reservationId);

            if (reservation == null)
                return NotFound();

            _unitOfWork.Reservation.Remove(reservation);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {reservation.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

    }
}
