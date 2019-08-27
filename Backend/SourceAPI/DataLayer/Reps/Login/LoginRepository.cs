using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Models;
using AttendanceApi.Environment;
using AttendanceApi.DataLayer.ViewModel;

namespace AttendanceApi.Reps
{
    public class LoginRepository : ILoginRepository
    {
        private AtdDbContext dbContext;
        public LoginRepository(AtdDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<RepResult<PersonnelVM>> Login(string nationalCode, string passWord)
        {
            if (dbContext != null)
            {

                var query = from prs in dbContext.Personnels join user in dbContext.Users on prs.PersonnelId equals user.PersonnelId
                          where prs.NationalCode == nationalCode && user.Password == passWord select prs;

               
                if(await query.AnyAsync())
                {
                    var obj = await query.Include(c => c.Users).SingleOrDefaultAsync();
                    PersonnelRepository.LoadUser(obj);
                    return new RepResult<PersonnelVM> { Successed = true, ResultObject = PersonnelVM.GetVM(obj) };
                }
                  else
                {
                    return new RepResult<PersonnelVM> {
                     Successed = false,
                      Message = "پرسنل با این کد ملی و پسورد در سیستم موجود نمی باشد" };
                }
            }

            return new RepResult<PersonnelVM>();
        }

        public async Task<RepResult<PersonnelVM>> Attend(string nationalCole)    {
            Console.Write("\n################NATIONAL CODE################\n" + nationalCole + "\n");
            Console.Write("\n################ Contect ################\n" + dbContext + "\n");
            if (dbContext != null) {
                Console.Write("\n################Got into the first if################\n");
                var query = from prs in dbContext.Personnels join user in dbContext.Users on prs.PersonnelId equals user.PersonnelId
                    where prs.NationalCode == nationalCole select prs;

            if (await query.AnyAsync()) {
                Console.Write("\n################Got into the second if################\n");
                var obj = await query.Include(c => c.Users).SingleOrDefaultAsync();
                PersonnelRepository.LoadUser(obj);
                return new RepResult<PersonnelVM> { Successed = true };
            } else {
                Console.Write("\n################Got into the second else of second if################\n");
                return new RepResult<PersonnelVM> {
                    Successed = false,
                };
            }

            }
            Console.Write("\n################Not Got to the first if################\n" + nationalCole + "\n");
            return new RepResult<PersonnelVM>();
        }
    }
}
