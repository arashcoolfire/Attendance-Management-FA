using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using AttendanceApi.Domain.Enums;

namespace AttendanceApi.Domain.Models
{
    public class AttendanceSpan
    {
        public AttendanceSpan()
        {
            ActionRequests = new HashSet<ActionRequest>();
        }
        public long AttendanceSpanId { get; set; }
        /// <summary>
        /// WorkTimeType (1 AtWork, 2 LeaveWork, 3 Mission, 4 OverTime, 5 Holiday)
        /// </summary>
        public WorkTimeType TimeType { get; set; }

        [ForeignKey("StartAttendanceTimeId")]
        public AttendanceTime StartAttendanceTime { set; get; }
        [ForeignKey("EndAttendanceTimeId")]
        public AttendanceTime EndAttendanceTime { set; get; }
        [ForeignKey("DayId")]
        public virtual Day Day { set; get; }
        public ICollection<ActionRequest> ActionRequests { set; get; }
    }
}
