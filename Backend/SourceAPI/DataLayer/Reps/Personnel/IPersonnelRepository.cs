using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Reps
{
    public interface IPersonnelRepository
    {
        Task<RepResult<IEnumerable<PersonnelVM>>> Get();
        Task<RepResult<PersonnelVM>> Get(int id);
        Task<RepResult<PersonnelVM>> Add(PersonnelVM obj);
        Task<RepResult<PersonnelVM>> Update(PersonnelVM obj);
        Task<RepResult<int>> Delete(int id);
    }
}
