using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;

namespace AttendanceApi.DataLayer.ViewModel
{
    public class AttendanceSpanVM
    {
        public long AttendanceSpanId { get; set; }
        public WorkTimeType TimeType { get; set; }
        public string TimeTypeName { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Duration { get; set; }
        public long DurationSecends { get; set; }

        private static PersianDateTime perDtStart;
        private static PersianDateTime perDtEnd;
        public static AttendanceSpanVM GetVM(AttendanceSpan span)
        {
            var vm = new AttendanceSpanVM();
            vm.AttendanceSpanId = span.AttendanceSpanId;
            vm.TimeType = span.TimeType;
            vm.TimeTypeName = EnumName.GetWorkTimeType(span.TimeType);

            perDtStart = new PersianDateTime(span.StartAttendanceTime.DateTime);
            vm.StartTime = perDtStart.ToString("hh:mm:ss tt");

            if (span.EndAttendanceTime != null)
            {
                perDtEnd = new PersianDateTime(span.EndAttendanceTime.DateTime);
                vm.EndTime = perDtEnd.ToString("hh:mm:ss tt");

                TimeSpan sp = span.EndAttendanceTime.DateTime.Subtract(span.StartAttendanceTime.DateTime);
                vm.DurationSecends = System.Convert.ToInt64(sp.TotalSeconds);
                vm.Duration = Environment.Convert.TimeSpanToReadableString(sp);
            }

            return vm;
        }

        internal static ICollection<AttendanceSpanVM> GetVMs(List<AttendanceSpan> spans)
        {
            List<AttendanceSpanVM> list = new List<AttendanceSpanVM>();
            foreach (var item in spans)
                list.Add(GetVM(item));

            return list;
        }
 
    }
}
