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
    public class BranchesController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BranchesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetAll()
        {
            return Ok(_unitOfWork.Branch.GetAll());
        }

        [HttpGet("{branchId}", Name = "GetBranchById")]
        [AllowAnonymous]
        public IActionResult GetById(int branchId)
        {
            var obj = _unitOfWork.Branch.GetFirstOrDefault(b => b.Id == branchId);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = _mapper.Map<BranchDto>(obj);

            return Ok(objDto);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(BranchDto dto)
        {
            if (dto == null) return BadRequest(ModelState);

            var branch = _mapper.Map<Branch>(dto);
            _unitOfWork.Branch.Add(branch);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when saving the record {branch.Name}");
                return StatusCode(500, ModelState);
            }

            // return Ok(dto);
            return CreatedAtRoute("GetBranchById", new { branchId = branch.Id }, branch);
        }
        [HttpPatch("{branchId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int branchId, BranchDto dto)
        {
            if (dto == null || branchId != dto.Id) return BadRequest(ModelState);

            var branch = _mapper.Map<Branch>(dto);
            _unitOfWork.Branch.Update(branch);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when updating the record {branch.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{branchId:int}")]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int branchId)
        {
            var branch = _unitOfWork.Branch.GetFirstOrDefault(b => b.Id == branchId);

            if (branch == null)
                return NotFound();

            _unitOfWork.Branch.Remove(branch);

            if (!_unitOfWork.Save())
            {
                ModelState.AddModelError("", $"Something went wrong when deleting the record {branch.Name}");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}
