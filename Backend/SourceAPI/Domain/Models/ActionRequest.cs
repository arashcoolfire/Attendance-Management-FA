using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AttendanceApi.Domain.Enums;

namespace AttendanceApi.Domain.Models
{
    public class ActionRequest
    {
        public long ActionRequestId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime RegisterDate { get; set; }
    
        public virtual AttendanceSpan AttendanceSpan { set; get; }
        public virtual Day Day { set; get; }

        /// <summary>
        /// WorkTimeType (2 LeaveWork, 3 Mission, 4 OverTime)
        /// </summary>
        public WorkTimeType RequestType { get; set; }
        [ForeignKey("ApplicantPersonnelId")]
        public virtual Personnel Applicant { set; get; }
        [ForeignKey("CorroborantPersonnelId")]
        public virtual Personnel Corroborant { set; get; }
        [StringLength(250)]
        public string DescriptionOfApplicant { get; set; }
        [StringLength(250)]
        public string DescriptionOfCorroborant { get; set; }


        [NotMapped]
        public string RequestTypeName { get; set; }
        [NotMapped]
        public string RequestStatusName { get; set; }
    }
}
