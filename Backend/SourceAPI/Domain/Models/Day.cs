using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AttendanceApi.Domain.Enums;

namespace AttendanceApi.Domain.Models
{
    public class Day
    {
        public Day()
        {
            AttendanceSpans = new HashSet<AttendanceSpan>();
            ActionRequests = new HashSet<ActionRequest>();
        }
        public long DayId { get; set; }
        public DateTime Date { get; set; }
        [ForeignKey("PersonnelId")]
        public int PersonnelId { get; set; }
        public virtual Personnel Personnel { set; get; }
        public ICollection<AttendanceSpan> AttendanceSpans { set; get; }
        public ICollection<ActionRequest> ActionRequests { set; get; }
        /// <summary>
        /// WorkTimeType (1 AtWork, 2 LeaveWork, 3 Mission, 4 OverTime, 5 Holiday)
        /// </summary>
        public WorkTimeType TimeType { get; set; }
    }
}
