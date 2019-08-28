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
    public class AttendanceController : ControllerBase
    {
        private IAttendanceTimeRepository atdTimeRep;
        private IDayRepository dayRep;
        private IMonthRepository monthRep;
        private ILoginRepository loginRep;
        private IConfiguration configuration { get; set; }

        public AttendanceController(IConfiguration configuration, IAttendanceTimeRepository atdTimeRep, IDayRepository dayRep, IMonthRepository monthRep, ILoginRepository loginRep)
        {
            this.atdTimeRep = atdTimeRep;
            this.dayRep = dayRep;
            this.monthRep = monthRep;
            this.loginRep = loginRep;

            this.configuration = configuration;
        }

        [HttpPost("AddTime")]
        public async Task<IActionResult> AddTime([FromBody]JObject data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var personnelId = data["PersonnelId"].Value<int>();
                    var timeType = data["TimeType"].Value<int>();

                    if(timeType <= 0 || timeType >= 5)
                    {
                        var res = new RepResult<Day> { Successed = false, Message = "نوع زمان ورودی مجاز نمی باشد", ResultObject = null }; ;
                        return BadRequest(res);
                    }
                    var dateTime = DateTime.Now;
                    var obj = (await atdTimeRep.Add(personnelId, dateTime,(WorkTimeType)timeType));

                    if (obj.Successed)
                    {
                        var res = await dayRep.GetDayData(personnelId, dateTime);
                        if (res.Successed)
                            return Ok(res.ResultObject);
                        else
                            return NotFound(res);
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

        [HttpPost("AddTimeByNationalCode")]
        public async Task<IActionResult> AddTimeByNationalCode([FromBody]JObject data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var nationalCode = data["NationalCode"].Value<string>();
                    var timeType = data["TimeType"].Value<int>();

                    if (timeType <= 0 || timeType >= 5)
                    {
                        var res = new RepResult<Day> { Successed = false, Message = "نوع زمان ورودی مجاز نمی باشد", ResultObject = null }; ;
                        return BadRequest(res);
                    }

                    var resLogin = await loginRep.Login(nationalCode);
                    if(resLogin.Successed)
                    {
                        var dateTime = DateTime.Now;
                        var obj = (await atdTimeRep.Add(resLogin.ResultObject.PersonnelId, dateTime, (WorkTimeType)timeType));

                        if (obj.Successed)
                        {
                            var res = await dayRep.GetDayData(resLogin.ResultObject.PersonnelId, dateTime);
                            if (res.Successed)
                                return Ok(res.ResultObject);
                            else
                                return NotFound(res);
                        }
                        else
                        {
                            return NotFound(obj);
                        }
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpPost("SaveDayData")]
        public async Task<IActionResult> SaveDayData([FromBody]JObject data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var dayId = data["dayId"].Value<long>();
                    var personnelId = data["personnelId"].Value<int>();
                    var timeType = data["timeType"].Value<int>();
                    var spans = data["spans"].Value<JArray>();

                    var rawSpans = new List<RawSpanVM>();
                    foreach (var item in spans)
                    {
                        rawSpans.Add(new RawSpanVM {TimeType = (WorkTimeType)(
                            item["timeType"].Value<int>()),
                            StartTime = item["startTime"].Value<string>(),
                            EndTime = item["endTime"].Value<string>()
                        });
                    }

                    var obj = (await dayRep.SaveDay(dayId, personnelId, timeType, rawSpans));

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


        [HttpGet("GetDayData/{personnelId}/{shamsiDayDate}")]
        public async Task<IActionResult> GetDayData(int personnelId, string shamsiDayDate)
        {
            try
            {
                PersianDateTime prsDt = PersianDateTime.Parse(shamsiDayDate.Replace("-", "/"));

                var obj = (await dayRep.GetDayData(personnelId, prsDt.ToDateTime()));

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

        [HttpGet("GetMonthData/{personnelId}/{shamsiMonthDate}")]
        public async Task<IActionResult> GetMonthData(int personnelId, string shamsiMonthDate)
        {
            try
            {
                var obj = (await monthRep.GetMonthData(personnelId, shamsiMonthDate));

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
    }
}
