using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Reps
{
    public interface IDeveloperRepository
    {
        Task<RepResult<bool>> ReNewDatabse();
    }
}
