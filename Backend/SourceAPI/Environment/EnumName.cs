using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;

namespace AttendanceApi.Environment
{
    public static class EnumName
    {
        public static string GetWorkTimeType(WorkTimeType timeType)
        {
            var name = "";

            switch (timeType)
            {
                case WorkTimeType.AtWork:
                    name = "کار اداری";
                    break;
                case WorkTimeType.LeaveWork:
                    name = "مرخصی";
                    break;
                case WorkTimeType.Mission:
                    name = "ماموریت";
                    break;
                case WorkTimeType.OverTime:
                    name = "اضافه کاری";
                    break;
                case WorkTimeType.Holiday:
                    name = "تعطیل";
                    break;
            }

            return name;
        }


    }
}
