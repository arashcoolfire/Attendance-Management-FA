using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AttendanceApi.Reps;
using AttendanceApi.Domain.Models;
using AttendanceApi.Domain.Enums;
using Newtonsoft.Json.Linq;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeveloperController : ControllerBase
    {
        private IDeveloperRepository devrep;

        private IConfiguration configuration { get; set; }

        public DeveloperController(IConfiguration configuration, IDeveloperRepository devrep)
        {
            this.devrep = devrep;

            this.configuration = configuration;
        }

        [HttpPost("ReNewDatabse")]
        public async Task<IActionResult> ReNewDatabse()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dateTime = DateTime.Now;
                    var obj = (await devrep.ReNewDatabse());

                    if (obj.Successed)
                    {
                        return Ok(obj.ResultObject);
                    }
                    else
                    {
                        return NotFound(obj);
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }
    }
}
