using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace WebApplication18.Domain.IModels
{
    public interface IDay
    {
        long DayId { get; set; }
        int PersonnelId { get; set; }
        DateTime Date { get; set; }
        bool Dayoff { get; set; }

        ICollection<IAttendanceSpan> AttendanceSpans { set; get; }
    }
}
