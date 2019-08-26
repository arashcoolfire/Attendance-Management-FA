using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Reps
{
    public interface ILoginRepository
    {
        Task<RepResult<PersonnelVM>> Login(string nationalCode, string passWord);
        Task<RepResult<PersonnelVM>> Login(string nationalCode);
    }
}
