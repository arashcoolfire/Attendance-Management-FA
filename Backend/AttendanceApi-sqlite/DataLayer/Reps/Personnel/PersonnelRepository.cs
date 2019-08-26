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
    public class PersonnelRepository : IPersonnelRepository
    {
        private AtdDbContext dbContext;
        private DbSet<Personnel> entity;
        public PersonnelRepository(AtdDbContext dbContext)
        {
            this.dbContext = dbContext;
            entity = dbContext.Set<Personnel>();
        }

        public async Task<RepResult<IEnumerable<PersonnelVM>>> Get()
        {
            if (dbContext != null)
            {
                var objs = await entity.Include(c => c.Users).ToAsyncEnumerable().ToList();

                foreach (Personnel obj in objs)
                    LoadUser(obj);

                return new RepResult<IEnumerable<PersonnelVM>> { Successed = true, ResultObject = objs.Select(m => PersonnelVM.GetVM(m)) };
            }

            return new RepResult<IEnumerable<PersonnelVM>>();
        }

        public async Task<RepResult<PersonnelVM>> Get(int id)
        {
            if (dbContext != null)
            {
                var obj = await entity.Include(c => c.Users).SingleOrDefaultAsync(s => s.PersonnelId == id);
                LoadUser(obj);

                return new RepResult<PersonnelVM> { Successed = true, ResultObject = PersonnelVM.GetVM(obj) };
            }

            return new RepResult<PersonnelVM>();
        }

        public async Task<RepResult<PersonnelVM>> Add(PersonnelVM vm)
        {
            if (dbContext != null)
            {
                if (!await dbContext.Personnels.AnyAsync(a => a.NationalCode.Contains(vm.NationalCode)))
                {
                    var obj = PersonnelVM.GetAnyObject(vm, true);
                    dbContext.Entry(obj.Item1).State = EntityState.Added;
                    dbContext.Entry(obj.Item2).State = EntityState.Added;
                    //await dbContext.Personnels.AddAsync(obj.Item1);
                    //await dbContext.Users.AddAsync(obj.Item2);
                    await dbContext.SaveChangesAsync();

                    return new RepResult<PersonnelVM> { Successed = true, ResultObject = (await Get(obj.Item1.PersonnelId)).ResultObject };
                }
                else
                {
                    return new RepResult<PersonnelVM> { Successed = false, Message = "پرسنل با این کد ملی در سیستم ثبت شده است. شما قادر به ثبت مجدد نمی باشید", ResultObject = null };
                }
            }

            return new RepResult<PersonnelVM>();
        }

        public async Task<RepResult<PersonnelVM>> Update(PersonnelVM vm)
        {
            if (dbContext != null)
            {
                if (!await dbContext.Personnels.AnyAsync(a => a.NationalCode.Contains(vm.NationalCode) && a.PersonnelId != vm.PersonnelId))
                {
                    var obj = PersonnelVM.GetAnyObject(vm, false);

                    dbContext.Entry(obj.Item1).State = EntityState.Modified;
                    dbContext.Entry(obj.Item2).State = EntityState.Modified;

                    await dbContext.SaveChangesAsync();

                    return new RepResult<PersonnelVM> { Successed = true, ResultObject = (await Get(obj.Item1.PersonnelId)).ResultObject };
                }
                else
                {
                    return new RepResult<PersonnelVM> { Successed = false, Message = "پرسنل با این کد ملی در سیستم ثبت شده است. شما قادر به ثبت مجدد نمی باشید", ResultObject = null };
                }
            }

            return new RepResult<PersonnelVM>();
        }

        public async Task<RepResult<int>> Delete(int id)
        {
            int result = 0;

            if (dbContext != null)
            {
                var obj = await dbContext.Personnels.FirstOrDefaultAsync(x => x.PersonnelId == id);

                if (obj != null)
                {
                    dbContext.Personnels.Remove(obj);

                    result = await dbContext.SaveChangesAsync();

                    return new RepResult<int> { Successed = true, ResultObject = result };
                }
            }

            return new RepResult<int> { Successed = false, ResultObject = result };
        }

        public static void LoadUser(Personnel personnel)
        {
            if (personnel.Users.Count() > 0)
            {
                var user = personnel.Users.SingleOrDefault();
                if (user != null)
                {
                    personnel.HasCurrentUser = true;
                    personnel.CurrentUser = user;
                }
            }
        }
    }
}
