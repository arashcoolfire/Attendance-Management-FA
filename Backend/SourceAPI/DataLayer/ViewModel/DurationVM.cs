using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApi.DataLayer.ViewModel
{
    public class DurationVM
    {
        public long DurationOfWorkSecends { get; set; }
        public string DurationOfWork { get; set; }

        public long DurationOfLeaveSecends { get; set; }
        public string DurationOfLeave { get; set; }

        public long DurationOfMisiionSecends { get; set; }
        public string DurationOfMisiion { get; set; }

        public long DurationOfOverTimeSecends { get; set; }
        public string DurationOfOverTime { get; set; }
    }
}