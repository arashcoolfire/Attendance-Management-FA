using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
 

namespace AttendanceApi.Reps
{
    public interface IAttendanceSpanRepository
    {
        Task<RepResult<bool>> GenerateSpans(int personnelId,DateTime dayDate);
        Task<RepResult<bool>> RemoveSpans(int personnelId, DateTime dayDate);

        Task<RepResult<IEnumerable<AttendanceSpan>>> GetSpans(Day day);

        Task<Day> GetDayModel(int personnelId, DateTime dayDate);
    }
}
