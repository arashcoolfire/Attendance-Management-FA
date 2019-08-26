using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.DataLayer.ViewModel;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
 

namespace AttendanceApi.Reps
{
    public interface IDayRepository
    {
        Task<RepResult<DayVM>> GetDayData(int personnelId, DateTime dayDate);

        Task<RepResult<DayVM>> GetDayData(AttendanceTime attendanceTime);

        Task<RepResult<DayVM>> SaveDay(long dayId, int PersonnelId, int timeType, List<RawSpanVM> rawSpans);
    }
}
