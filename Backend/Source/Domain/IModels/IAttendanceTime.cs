using System;

namespace WebApplication18.Domain.IModels
{
    public interface IAttendanceTime
    {
        long AttendanceTimeId { get; set; }
        DateTime DateTime { get; set; }
    }
}
