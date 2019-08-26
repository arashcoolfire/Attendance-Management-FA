using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
 

namespace AttendanceApi.Reps
{
    public class AttendanceSpanRepository : IAttendanceSpanRepository
    {
        private AtdDbContext dbContext;
        PersianCalendar persianCalendar = new PersianCalendar();

        public AttendanceSpanRepository(AtdDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RepResult<bool>> GenerateSpans(int personnelId, DateTime dayDate)
        {
            if (dbContext != null)
            {
                await RemoveSpans(personnelId, dayDate);

                var qTimes = from a in dbContext.AttendanceTimes
                            where a.PersonnelId == personnelId && a.DateTime.Date == dayDate.Date.Date
                            orderby a.DateTime
                            select a;


                var day = await GetDayModel(personnelId, dayDate);

                var times = await qTimes.ToListAsync();
                if (times.Count > 0)
                {
                    AttendanceSpan atdSpan = new AttendanceSpan();
                    List<AttendanceSpan> spanlist = new List<AttendanceSpan>();
                    foreach (var item in times.Select((value, index) => (value, index)))
                    {
                        // first span item
                        if(item.index % 2 == 0)
                        {
                            atdSpan = new AttendanceSpan();
                            atdSpan.TimeType = item.value.TimeType;

                            atdSpan.StartAttendanceTime = item.value;
                            atdSpan.Day = day;

                            if(item.index == times.Count - 1)
                                spanlist.Add(atdSpan);
                        }

                        // last span item
                        if (item.index % 2 != 0)
                        {
                            atdSpan.EndAttendanceTime = item.value;
                            spanlist.Add(atdSpan);
                        }
                    }

                    await dbContext.AttendanceSpans.AddRangeAsync(spanlist);
                    await dbContext.SaveChangesAsync();

                    var res = await GetSpans(day);

                    if (res.Successed)
                        return new RepResult<bool> { Successed = true, ResultObject = true };
                }
            }

            return new RepResult<bool> ();
        }
        public async Task<RepResult<bool>> RemoveSpans(int personnelId, DateTime dayDate)
        {
            if (dbContext != null)
            {
                var qSpans = from d in dbContext.Days
                             join span in dbContext.AttendanceSpans
                             on d.DayId equals span.Day.DayId
                             where d.PersonnelId == personnelId && d.Date.Date == dayDate.Date.Date
                             select span;

                dbContext.AttendanceSpans.RemoveRange(await qSpans.ToListAsync());
                await dbContext.SaveChangesAsync();

                return new RepResult<bool> { Successed = true, ResultObject = true };
            }

            return new RepResult<bool>();
        }
        public async Task<RepResult<IEnumerable<AttendanceSpan>>> GetSpans(Day day)
        {
            if (dbContext != null)
            {
                var qSpans = from d in dbContext.Days
                             join span in dbContext.AttendanceSpans
                             on d.DayId equals span.Day.DayId
                             where d.PersonnelId == day.PersonnelId && d.Date.Date == day.Date.Date.Date
                             orderby span.StartAttendanceTime
                             select span;


                qSpans = qSpans.Include(c => c.StartAttendanceTime).Include(c => c.EndAttendanceTime);

                return new RepResult<IEnumerable<AttendanceSpan>> { Successed = true, ResultObject = await qSpans.ToListAsync() };
            }

            return new RepResult<IEnumerable<AttendanceSpan>>();
        }

        public async Task<Day> GetDayModel(int personnelId, DateTime dayDate)
        {
            var qDay = from d in dbContext.Days
                       where d.PersonnelId == personnelId &&
                       d.Date.Date == dayDate.Date
                       select d;

            if (!await qDay.AnyAsync())
            {
                Day newDay = new Day();
                newDay.PersonnelId = personnelId;
                newDay.Date = dayDate;

                // todo arash : set day event by calendar and app setting

                var dayOfWeek = persianCalendar.GetDayOfWeek(dayDate);
                if (dayOfWeek == DayOfWeek.Friday)
                    newDay.TimeType = WorkTimeType.Holiday;
                else
                    newDay.TimeType = WorkTimeType.AtWork;

                await dbContext.Days.AddAsync(newDay);
                await dbContext.SaveChangesAsync();
            }

            return await qDay.FirstOrDefaultAsync();
        }
    }
}
