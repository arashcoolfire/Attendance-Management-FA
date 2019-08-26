using System.Collections.Generic;

namespace WebApplication18.Domain.IModels
{
    public interface IPersonnel
    {
        int PersonnelId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string NationalCode { get; set; }
        string PhoneNo { get; set; }
        string Email { get; set; }

        ICollection<IUser> Users { get; set; }
        ICollection<IAttendanceTime> AttendanceTimes { get; set; }
    }
}
