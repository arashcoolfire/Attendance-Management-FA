using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain;
using AttendanceApi.Domain.Models;

namespace AttendanceApi.DataLayer
{
    public static class DbInitializer
    {
        public static void Seed(AtdDbContext context)
        {
            if (!context.Personnels.Any())
            {
                var personnel1 = new Personnel
                {
                    FirstName = "Arash",
                    LastName = "Tafakori",
                    NationalCode = "1234512345",
                    PhoneNo = "09129264862",
                    Email = "arashcoolfire@gmail.com",
                };
                var personnel2 = new Personnel
                {
                    FirstName = "Alireza",
                    LastName = "razi Nejhad",
                    NationalCode = "4444455555",
                    PhoneNo = "09354801265",
                    Email = "alirezarazinejhad@gmail.com",
                };

                context.Personnels.AddRange(personnel1, personnel2);
    
                var user1 = new User
                {
                    UserName = "admin",
                    Password = "123",
                    UserRole = AttendanceApi.Domain.Enums.UserRole.Admin,
                    UserActived = true
                };
                var user2 = new User
                {
                    UserName = "admin",
                    Password = "123",
                    UserRole = AttendanceApi.Domain.Enums.UserRole.Admin,
                    UserActived = true
                };
                user1.Personnel = personnel1;
                user2.Personnel = personnel2;

                context.Users.AddRange(user1, user2);
                context.SaveChanges();
            }
        }
    }
}
