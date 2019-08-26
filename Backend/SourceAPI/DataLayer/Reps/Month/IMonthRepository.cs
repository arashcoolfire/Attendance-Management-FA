using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.DataLayer.ViewModel;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
 

namespace AttendanceApi.Reps
{
    public interface IMonthRepository
    {
        Task<RepResult<MonthVM>> GetMonthData(int personnelId, string shamsiMonthDate);
    }
}
