using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Reps
{
    public class AttendanceTimeRepository : IAttendanceTimeRepository
    {
        private AtdDbContext dbContext;
        private IAttendanceSpanRepository spanRep;
        public AttendanceTimeRepository(AtdDbContext dbContext, IAttendanceSpanRepository spanRep)
        {
            this.dbContext = dbContext;
            this.spanRep = spanRep;
        }

        public async Task<RepResult<bool>> Add(int personnelId, DateTime dateTime, WorkTimeType timeType)
        {
            if (dbContext != null)
            {
                var resAdd = await InsertAttendanceTime(personnelId, timeType, dateTime);

                if(resAdd.Successed)
                {
                    await spanRep.GenerateSpans(personnelId, dateTime);

                    return new RepResult<bool> { Successed = true, ResultObject = true };
                }
            }

            return new RepResult<bool>();
        }

        public async Task<RepResult<bool>> InsertAttendanceTime(int personnelId, WorkTimeType timeType, DateTime dateTime)
        {
            if (dbContext != null)
            {
                var query = from a in dbContext.AttendanceTimes
                            where a.PersonnelId == personnelId && a.DateTime.Date == dateTime.Date
                            orderby a.DateTime descending
                            select a;

                var enumer = query.ToAsyncEnumerable();
                var any = await enumer.Any();
                var count = await enumer.Count();

                if (any)
                {
                    var lastObj = await query.FirstOrDefaultAsync();
                    if (lastObj.DateTime > dateTime)
                    {
                        return new RepResult<bool> { Successed = false, Message = "زمان که برای ثبت تعیین شده از آخرین زمان که در سیستم ثبت شده کوچکتر است ", ResultObject = false };
                    }
                }

                if (any && (count % 2) == 1)
                {
                    var lastObj = await query.FirstOrDefaultAsync();

                    if (lastObj.TimeType != timeType)
                    {
                        if (timeType == WorkTimeType.LeaveWork ||
                            timeType == WorkTimeType.Mission ||
                            timeType == WorkTimeType.OverTime)
                        {
                            await dbContext.AttendanceTimes.AddAsync(new AttendanceTime()
                            {
                                PersonnelId = personnelId,
                                DateTime = dateTime,
                                TimeType = lastObj.TimeType
                            });
                        }
                    }
                }

                await dbContext.AttendanceTimes.AddAsync(new AttendanceTime()
                {
                    PersonnelId = personnelId,
                    DateTime = dateTime.AddSeconds(1),
                    TimeType = timeType
                });

                await dbContext.SaveChangesAsync();

                return new RepResult<bool> { Successed = true, ResultObject = true };
            }

            return new RepResult<bool>();
        }

        public async Task<RepResult<bool>> RemoveAttendanceTimes(int personnelId, DateTime dayDate)
        {
            if (dbContext != null)
            {
                var query = from a in dbContext.AttendanceTimes
                             where a.PersonnelId == personnelId && a.DateTime.Date == dayDate.Date.Date
                             select a;

                dbContext.AttendanceTimes.RemoveRange(await query.ToListAsync());
                await dbContext.SaveChangesAsync();

                return new RepResult<bool> { Successed = true, ResultObject = true };
            }

            return new RepResult<bool>();
        }

    }
}
