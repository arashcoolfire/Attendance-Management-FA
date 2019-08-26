using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;
using AttendanceApi.DataLayer;

namespace AttendanceApi.Reps
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private AtdDbContext dbContext;
        public DeveloperRepository(AtdDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RepResult<bool>> ReNewDatabse()
        {
            if (dbContext != null)
            {
                dbContext.AttendanceSpans.RemoveRange(dbContext.AttendanceSpans.ToList());
                dbContext.AttendanceTimes.RemoveRange(dbContext.AttendanceTimes.ToList());
                dbContext.Days.RemoveRange(dbContext.Days.ToList());
                dbContext.ActionRequests.RemoveRange(dbContext.ActionRequests.ToList());
                dbContext.Personnels.RemoveRange(dbContext.Personnels.ToList());
                dbContext.Users.RemoveRange(dbContext.Users.ToList());

                await dbContext.SaveChangesAsync();

                DbInitializer.Seed(dbContext);

                return new RepResult<bool> { Successed = true, ResultObject = true };
            }

            return new RepResult<bool>();
        }
    }
}
