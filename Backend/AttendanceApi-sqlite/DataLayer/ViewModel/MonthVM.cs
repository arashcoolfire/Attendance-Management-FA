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
    public class MonthVM
    {
        public string Year { get; set; }
        public string MonthName { get; set; }
        public int PersonnelId { get; set; }
        public DurationVM Duration { get; set; }
        public ICollection<DayVM> Days { set; get; }
    }
}
