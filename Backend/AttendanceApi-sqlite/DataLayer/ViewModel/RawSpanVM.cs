using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;

namespace AttendanceApi.DataLayer.ViewModel
{
    public class RawSpanVM
    {
        public WorkTimeType TimeType { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
     }
}
