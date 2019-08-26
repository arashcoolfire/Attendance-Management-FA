using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;
using AttendanceApi.Domain.Models;

namespace AttendanceApi.DataLayer.ViewModel
{
    public class PersonnelVM
    {
        #region Persnnel
        public int PersonnelId { get; set; }
        [Required(ErrorMessage = "")]
        [Display(Name = "نام")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "")]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "")]
        [Display(Name = "کد ملی")]
        public string NationalCode { get; set; }
        [Display(Name = "موبایل")]
        public string PhoneNo { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        #endregion

        #region User
        public int? UserId { get; set; }
        [Display(Name = "پسورد")]
        public string Password { get; set; }
        public UserRole? UserRole { get; set; }
        [Display(Name = "نقش")]
        public string UserRoleName { get; set; }
        // public bool? UserActived { get; set; }
        //[Display(Name = "وضعیت")]
        //public string UserActivedName { get; set; }
        #endregion

        public static PersonnelVM GetVM(Personnel obj)
        {
            PersonnelVM vm = new PersonnelVM();

            vm.PersonnelId = obj.PersonnelId;
            vm.FirstName = obj.FirstName;
            vm.LastName = obj.LastName;
            vm.NationalCode = obj.NationalCode;
            vm.PhoneNo = obj.PhoneNo;
            vm.Email = obj.Email;
            vm.UserId = obj.CurrentUser?.UserId;
            vm.Password = obj.CurrentUser?.Password;
            vm.UserRole = obj.CurrentUser?.UserRole;
            vm.UserRoleName = obj.CurrentUser != null ? (obj.CurrentUser.UserRole == Domain.Enums.UserRole.Admin ? "مدیر سیستم" : "کاربر سیستم") : null;
            //vm.UserActived = obj.CurrentUser?.UserActived;
            //vm.UserActivedName = (obj.CurrentUser.UserActived ? "فعال" : "غیر فعال");

            return vm;
        }

        private static Personnel GetPersonnel(PersonnelVM vm, bool isAddObject)
        {
            Personnel obj = new Personnel();

            if (!isAddObject)
                obj.PersonnelId = vm.PersonnelId;
            obj.FirstName = vm.FirstName;
            obj.LastName = vm.LastName;
            obj.NationalCode = vm.NationalCode;
            obj.PhoneNo = vm.PhoneNo;
            obj.Email = vm.Email;
 
            return obj;
        }

        private static User GetUser(PersonnelVM vm, Personnel prs, bool isAddObject)
        {
            User user = new User();

            if (!isAddObject)
                user.UserId = vm.UserId.GetValueOrDefault();
            user.PersonnelId = prs.PersonnelId;
            user.Password = vm.Password;
            user.UserRole = vm.UserRole.GetValueOrDefault();
            user.UserActived = true;
            user.Personnel = prs;

            return user;
        }

        public static Tuple<Personnel, User> GetAnyObject(PersonnelVM vm, bool isAddObject)
        {
            Personnel prs = GetPersonnel(vm, isAddObject);
            User user = GetUser(vm, prs, isAddObject);
  
            return Tuple.Create(prs, user);
        }
    }
}
