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
        private ILoginRepository loginRep;
        private IAttendanceTimeRepository atdTimeRep;
        private IDayRepository dayRep;
        private IMonthRepository monthRep;
        private IConfiguration configuration { get; set; }

        public AttendanceController(IConfiguration configuration, IAttendanceTimeRepository atdTimeRep, IDayRepository dayRep, IMonthRepository monthRep)
        {
            this.atdTimeRep = atdTimeRep;
            this.dayRep = dayRep;
            this.monthRep = monthRep;

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
                // Console.WriteLine("incoming Data");
                Console.Write("\n################DATA##################\n" + data);
                try
                {
                    Console.Write("\n################NATIONAL ID################\n" + data["NationalCode"]);
                    Console.Write("\n################TIME TYPE################\n" + data["TimeType"].Value<int>());

                    var nationalCode = data["NationalCode"].Value<string>();
                    var timeType = data["TimeType"].Value<int>();

                    if (timeType <= 0 || timeType >= 5)
                    {
                    Console.Write("\n################BAD TIME TYPE################\n", timeType);
                        var res = new RepResult<Day> { Successed = false, Message = "نوع زمان ورودی مجاز نمی باشد", ResultObject = null }; ;
                        return BadRequest(res);
                    }
                    var dateTime = DateTime.Now;


                    var resulOfLogin = await loginRep.Attend(nationalCode);
                    Console.Write("\n################LOGINRES################\n", resulOfLogin);
                    if(resulOfLogin.Successed)
                    {
                        var personnelVm = resulOfLogin.ResultObject;

                        var obj = (await atdTimeRep.Add(personnelVm.PersonnelId, dateTime, (WorkTimeType)timeType));

                        if (obj.Successed)
                        {
                            var res = await dayRep.GetDayData(personnelVm.PersonnelId, dateTime);
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
                    else
                    {
                        return NotFound(resulOfLogin);
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            Console.WriteLine("ModelState is not Valid");
            Console.Write(ModelState);

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
