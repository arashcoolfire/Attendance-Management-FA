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
    public class LoginController : ControllerBase
    {
        private ILoginRepository objRep;
        private IConfiguration configuration { get; set; }

        public LoginController(IConfiguration configuration, ILoginRepository objRep)
        {
            this.objRep = objRep;
            this.configuration = configuration;
        }

        [HttpGet("{nationalCode}/{passWord}")]
        public async Task<IActionResult> Get(string nationalCode, string passWord)
        {
            try
            {
                var obj = (await objRep.Login(nationalCode, passWord));
                if (!obj.Successed)
                    return NotFound(obj);

                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("{nationalCode}")]
        public async Task<IActionResult> Get(string nationalCode)
        {
            try
            {
                var obj = (await objRep.Login(nationalCode));
                if (!obj.Successed)
                    return NotFound(obj);

                return Ok(obj);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
