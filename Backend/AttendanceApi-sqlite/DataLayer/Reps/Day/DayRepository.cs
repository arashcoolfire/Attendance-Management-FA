using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.DataLayer.ViewModel;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
 

namespace AttendanceApi.Reps
{
    public class DayRepository : IDayRepository
    {
        private AtdDbContext dbContext;
        private IAttendanceTimeRepository atdRep;
        private IAttendanceSpanRepository spanRep;
        public DayRepository(AtdDbContext dbContext, IAttendanceTimeRepository atdRep, IAttendanceSpanRepository spanRep)
        {
            this.dbContext = dbContext;
            this.atdRep = atdRep;
            this.spanRep = spanRep;
        }

        public async Task<RepResult<DayVM>> GetDayData(int personnelId, DateTime dayDate)
        {
            if (dbContext != null)
            {
                var day = await spanRep.GetDayModel(personnelId, dayDate);

                day.AttendanceSpans = (await spanRep.GetSpans(day)).ResultObject.ToList();
                var vm = DayVM.GetVM(day);
                return new RepResult<DayVM> { Successed = true, ResultObject = vm };
            }

            return new RepResult<DayVM>();
        }

        public async Task<RepResult<DayVM>> GetDayData(AttendanceTime attendanceTime)
        {
            if (dbContext != null)
            {
                var qDay = from d in dbContext.Days
                           where d.PersonnelId == attendanceTime.PersonnelId &&
                           d.Date.Date == attendanceTime.DateTime.Date
                           select d;

                if(await qDay.AnyAsync())
                {
                    var day = await qDay.FirstOrDefaultAsync();

                    var res = await GetDayData(day.PersonnelId, day.Date);
                    if(res.Successed)
                        return new RepResult<DayVM> { Successed = true, ResultObject = res.ResultObject };
                }
          
                return new RepResult<DayVM> { Successed = true, ResultObject = null };
            }

            return new RepResult<DayVM>();
        }

        public async Task<RepResult<DayVM>> SaveDay(long dayId, int personnelId, int timeType, List<RawSpanVM> rawSpans)
        {
            if (dbContext != null)
            {
                var day = await (from d in dbContext.Days where d.DayId == dayId select d).FirstOrDefaultAsync();

                await spanRep.RemoveSpans(personnelId, day.Date);
                await atdRep.RemoveAttendanceTimes(personnelId, day.Date);

                day.TimeType = (WorkTimeType)timeType;

                foreach (var item in rawSpans)
                {
                    DateTime startDate = DateTime.ParseExact(item.StartTime, "HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime endDate = DateTime.ParseExact(item.EndTime, "HH:mm:ss", CultureInfo.InvariantCulture);

                    await atdRep.InsertAttendanceTime(personnelId, item.TimeType, startDate);
                    await atdRep.InsertAttendanceTime(personnelId, item.TimeType, endDate);
                }

                await spanRep.GenerateSpans(personnelId, day.Date);

                var res = await GetDayData(personnelId, day.Date);
                if (res.Successed)
                    return new RepResult<DayVM> { Successed = true, ResultObject = res.ResultObject };

                return new RepResult<DayVM> { Successed = true, ResultObject = null };
            }

            return new RepResult<DayVM>();
        }
    }
}
