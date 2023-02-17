using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using HrSystem.DTOs;
using HrSystem.IRepository;
using HrSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HrSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ManagerController> _logger;
        private readonly IMapper _mapper;


        public ManagerController(IUnitOfWork unitOfWork, ILogger<ManagerController> logger, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManagers()
        {
            try
            {
                var managers = await _unitOfWork.Managers.GetAll();
                var results = _mapper.Map<IList<ManagerDTO>>(managers);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetManagers)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [Authorize]
        [HttpGet("{id}", Name = "GetManager")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetManager(Guid id)
        {
            try
            {
                var manager = await _unitOfWork.Managers.Get(q => q.Id == id);
                var results = _mapper.Map<ManagerDTO>(manager);
                return Ok(results);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetManager)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateManager([FromBody] ManagerDTO managerDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid POST attempt in {nameof(CreateManager)}");
                return BadRequest(ModelState);
            }

            try
            {
                var manager = _mapper.Map<Manager>(managerDTO);
                await _unitOfWork.Managers.Insert(manager);
                await _unitOfWork.Save();

                return CreatedAtRoute("GetManager", new { id = manager.Id}, manager);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(CreateManager)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateManager(Guid id,  [FromBody] Manager managerDTO)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateManager)}");  
                return BadRequest(ModelState);
            }

            try
            {
                var manager = await _unitOfWork.Managers.Get(q => q.Id == id);
                if (manager == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateManager)}");
                    return BadRequest("Submitted data is invalid");
                }

                _mapper.Map(managerDTO, manager);
                _unitOfWork.Managers.Update(manager);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateManager)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteManager(Guid id)
        {
            try
            {
                var manager = await _unitOfWork.Managers.Get(q => q.Id == id);
                if (manager == null)
                {
                    _logger.LogError($"Invalid UPDATE attempt in {nameof(UpdateManager)}");
                    return BadRequest("Submitted data is invalid");
                }

                await _unitOfWork.Managers.Delete(id);
                await _unitOfWork.Save();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(UpdateManager)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
           
        }
    }
}