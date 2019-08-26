using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AttendanceApi.Reps;
using AttendanceApi.Domain.Models;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        public GeneralController()
        {
        }

        [HttpGet("GetDateTimeNow")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(DateTime.Now);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
