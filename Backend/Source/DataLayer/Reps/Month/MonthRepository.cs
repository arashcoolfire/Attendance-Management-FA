using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.DataLayer.ViewModel;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
 

namespace AttendanceApi.Reps
{
    public class MonthRepository : IMonthRepository
    {
        private AtdDbContext dbContext;
        private IDayRepository dayRep;
        public MonthRepository(AtdDbContext dbContext, IDayRepository dayRep)
        {
            this.dbContext = dbContext;
            this.dayRep = dayRep;
        }

        public async Task<RepResult<MonthVM>> GetMonthData(int personnelId, string shamsiMonthDate)
        {
            if (dbContext != null)
            {
                var monthVm = new MonthVM();
                monthVm.PersonnelId = personnelId;

                PersianDateTime perDt = PersianDateTime.Parse((shamsiMonthDate + "-01").Replace("-", "/"));
                DateTime firstDayDt = perDt.ToDateTime();
                
                PersianCalendar pc = new PersianCalendar();
                monthVm.Year = pc.GetYear(firstDayDt).ToString();
                monthVm.MonthName = getMonthName(pc.GetMonth(firstDayDt));

                var lenDays = pc.GetDaysInMonth(perDt.Year, perDt.Month);
                monthVm.Days = new List<DayVM>();
                monthVm.Duration = new DurationVM();
                for (int i = 1; i < lenDays; i++)
                {
                    var dayDate = new DateTime(firstDayDt.Year, firstDayDt.Month, firstDayDt.Day);
        
                    if (i > 1)
                        dayDate = dayDate.AddDays(i);

                    var dayVm = (await dayRep.GetDayData(personnelId, dayDate)).ResultObject;

                    monthVm.Days.Add(dayVm);

                    monthVm.Duration.DurationOfWorkSecends += dayVm.Duration.DurationOfWorkSecends;
                    monthVm.Duration.DurationOfLeaveSecends += dayVm.Duration.DurationOfLeaveSecends;
                    monthVm.Duration.DurationOfMisiionSecends += dayVm.Duration.DurationOfMisiionSecends;
                    monthVm.Duration.DurationOfOverTimeSecends += dayVm.Duration.DurationOfOverTimeSecends;
                }

                monthVm.Duration.DurationOfWork = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(monthVm.Duration.DurationOfWorkSecends));
                monthVm.Duration.DurationOfLeave = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(monthVm.Duration.DurationOfLeaveSecends));
                monthVm.Duration.DurationOfMisiion = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(monthVm.Duration.DurationOfMisiionSecends));
                monthVm.Duration.DurationOfOverTime = Environment.Convert.TimeSpanToReadableString(TimeSpan.FromSeconds(monthVm.Duration.DurationOfOverTimeSecends));


                return new RepResult<MonthVM> { Successed = true, ResultObject = monthVm };
            }

            return new RepResult<MonthVM>();
        }

        private string getMonthName(int month)
        {
            switch (month)
            {
                case 1: return "فررودين";
                case 2: return "ارديبهشت";
                case 3: return "خرداد";
                case 4: return "تير‏";
                case 5: return "مرداد";
                case 6: return "شهريور";
                case 7: return "مهر";
                case 8: return "آبان";
                case 9: return "آذر";
                case 10: return "دي";
                case 11: return "بهمن";
                case 12: return "اسفند";
                default: return "";
            }
        }
    }
}
