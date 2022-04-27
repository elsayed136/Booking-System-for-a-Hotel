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
    public class RoomTypesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoomTypesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.RoomType.GetAll());
        }

        [HttpGet("{roomTypeId}", Name = "GetRoomTypeById")]
        [AllowAnonymous]
        public IActionResult GetById(int roomTypeId)
        {
            var obj = _unitOfWork.RoomType.GetFirstOrDefault(b => b.Id == roomTypeId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<RoomTypeDto>(obj);

            return Ok(objDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(RoomTypeDto dto)
        {
            if (dto == null) return BadRequest(ModelState);

            var roomType = _mapper.Map<RoomType>(dto);
            _unitOfWork.RoomType.Add(roomType);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {roomType.Name}");
                return StatusCode(500, ModelState);
            }

            // return Ok(dto);
            return CreatedAtRoute("GetRoomTypeById", new { roomTypeId = roomType.Id }, roomType);
        }

        [HttpPatch("{roomTypeId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int roomTypeId, RoomTypeDto dto)
        {
            if (dto == null || roomTypeId != dto.Id) return BadRequest(ModelState);

            var roomType = _mapper.Map<RoomType>(dto);
            _unitOfWork.RoomType.Update(roomType);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {roomType.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{roomTypeId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int roomTypeId)
        {
            var roomType = _unitOfWork.RoomType.GetFirstOrDefault(b => b.Id == roomTypeId);

            if (roomType == null)
                return NotFound();

            _unitOfWork.RoomType.Remove(roomType);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {roomType.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
