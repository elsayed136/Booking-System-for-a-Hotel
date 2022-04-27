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
    public class RoomsController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Room.GetAll(null, new string[] { $"{nameof(RoomType)}", $"{nameof(Branch)}" }));
        }

        [HttpGet("{roomId}", Name = "GetRoomById")]
        [AllowAnonymous]
        public IActionResult GetById(int roomId)
        {
            var obj = _unitOfWork.Room.GetFirstOrDefault(b => b.Id == roomId,
                    new string[] {$"{nameof(RoomType)}", $"{nameof(Branch)}", }
                );
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<RoomDto>(obj);

            return Ok(objDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CreateRoomDto dto)
        {
            if (dto == null) return BadRequest(ModelState);

            var room = _mapper.Map<Room>(dto);
            _unitOfWork.Room.Add(room);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {room.Id}");
                return StatusCode(500, ModelState);
            }
            // return Ok(dto);
            return CreatedAtRoute("GetRoomById", new { roomId = room.Id }, room);
        }

        [HttpPatch("{roomId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int roomId, UpdateRoomDto dto)
        {
            if (dto == null || roomId != dto.Id) return BadRequest(ModelState);

            var room = _mapper.Map<Room>(dto);
            _unitOfWork.Room.Update(room);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {room.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{roomId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int roomId)
        {
            var room = _unitOfWork.Room.GetFirstOrDefault(b => b.Id == roomId);

            if (room == null)
                return NotFound();

            _unitOfWork.Room.Remove(room);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {room.Id}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpGet("Search")]
        [AllowAnonymous]
        public IActionResult SearchRooms(DateTime startDate, DateTime endDate)
        {
            var objList = _unitOfWork.Room.GetAvailableRooms(startDate, startDate);
            return Ok(objList);
        }
        [HttpGet("ReportPage", Name = "ReportPage")]
        [Authorize]
        public IActionResult ReportPage()
        {
            var objList = _unitOfWork.Room.ReportLike();
            return Ok(objList);
        }
    }
}
