using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;

namespace AttendanceApi.Domain.Models
{
    public class AttendanceTime
    {
        public long AttendanceTimeId { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("PersonnelId")]
        public int PersonnelId { get; set; }
        public virtual Personnel Personnel { set; get; }

        public WorkTimeType TimeType { get; set; }
    }
}
