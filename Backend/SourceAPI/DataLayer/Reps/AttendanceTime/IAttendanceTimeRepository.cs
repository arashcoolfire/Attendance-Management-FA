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
    public interface IAttendanceTimeRepository
    {
        Task<RepResult<bool>> Add(int personnelId, DateTime dateTime, WorkTimeType timeType);
        Task<RepResult<bool>> InsertAttendanceTime(int personnelId, WorkTimeType timeType, DateTime dateTime);
        Task<RepResult<bool>> RemoveAttendanceTimes(int personnelId, DateTime dayDate);
    }
}
