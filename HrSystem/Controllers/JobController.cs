using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HrSystem.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HrSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<JobController> _logger;

        public JobController(IUnitOfWork unitOfWork, ILogger<JobController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJobs()
        {
            try
            {
                var jobs = await _unitOfWork.Jobs.GetAll();
                return Ok(jobs);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetJobs)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetJob(Guid id)
        {
            try
            {
                var job = await _unitOfWork.Jobs.Get(q => q.Id == id);
                return Ok(job);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(GetJob)}");
                return StatusCode(500, "Internal Server Error. Please Try Again Later.");
            }
        }
    }
}