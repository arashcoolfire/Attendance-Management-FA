using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;

namespace AttendanceApi.DataLayer.ViewModel
{
    public class DayVM
    {
        public long DayId { get; set; }
        public WorkTimeType TimeType { get; set; }
        public string TimeTypeName { get; set; }
        public string DayName { get; set; }
        public string DayShortName { get; set; }
        public int PersonnelId { get; set; }
        public DurationVM Duration { get; set; }
        public ICollection<AttendanceSpanVM> Spans { set; get; }

        private static PersianDateTime persianDateTime;
        public static DayVM GetVM(Day day)
        {
            var vm = new DayVM();
            vm.DayId = day.DayId;
            vm.PersonnelId = day.PersonnelId;

            persianDateTime = new PersianDateTime(day.Date);
            vm.DayName = persianDateTime.ToString("dddd d MMMM yyyy");
            vm.DayShortName = persianDateTime.ToString("dddd d");

            vm.TimeType = day.TimeType;
            vm.TimeTypeName = EnumName.GetWorkTimeType(day.TimeType);
            vm.Spans = AttendanceSpanVM.GetVMs(day.AttendanceSpans.ToList());
            //
            vm.Duration = new DurationVM();
            foreach (var sp in vm.Spans)
            {
                if (sp.TimeType == WorkTimeType.AtWork)
                    vm.Duration.DurationOfWorkSecends += sp.DurationSecends;

                if (sp.TimeType == WorkTimeType.LeaveWork)
                    vm.Duration.DurationOfLeaveSecends += sp.DurationSecends;

                if (sp.TimeType == WorkTimeType.Mission)
                    vm.Duration.DurationOfMisiionSecends += sp.DurationSecends;

                if (sp.TimeType == WorkTimeType.OverTime)
                    vm.Duration.DurationOfOverTimeSecends += sp.DurationSecends;
            }

            vm.Duration.DurationOfWork = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(vm.Duration.DurationOfWorkSecends));
            vm.Duration.DurationOfLeave = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(vm.Duration.DurationOfLeaveSecends));
            vm.Duration.DurationOfMisiion = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(vm.Duration.DurationOfMisiionSecends));
            vm.Duration.DurationOfOverTime = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(vm.Duration.DurationOfOverTimeSecends));

            //
            return vm;
        }
    }
}
