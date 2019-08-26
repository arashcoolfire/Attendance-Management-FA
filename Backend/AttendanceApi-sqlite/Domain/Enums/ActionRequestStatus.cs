using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceApi.Domain.Enums
{
    public enum ActionRequestStatus
    {
        NewRequest = 1,
        Pending = 2,
        Confirmed = 3,
        NotConfirmed = 4
    }
}
