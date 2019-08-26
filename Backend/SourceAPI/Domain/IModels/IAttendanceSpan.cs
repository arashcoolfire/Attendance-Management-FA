using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication18.Domain.Enums;

namespace WebApplication18.Domain.IModels
{
    public interface IAttendanceSpan
    {
        long AttendanceSpanId { get; set; }
        long StartAttendanceTimeId { set; get; }
        long EndAttendanceTimeId { set; get; }
        SpanType SpanType { get; set; }
    }
}
