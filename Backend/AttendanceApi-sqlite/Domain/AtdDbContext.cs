using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Models;

namespace AttendanceApi.Domain
{
    public class AtdDbContext : DbContext
    {
        public AtdDbContext(DbContextOptions<AtdDbContext> options)
       : base(options)
        { }

        public AtdDbContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=adt.db");
        }
        public DbSet<Personnel> Personnels { set; get; }
        public DbSet<User> Users { set; get; }
        public DbSet<Day> Days { set; get; }
        public DbSet<AttendanceSpan> AttendanceSpans { set; get; }
        public DbSet<AttendanceTime> AttendanceTimes { set; get; }
        public DbSet<ActionRequest> ActionRequests { set; get; }
    }
}
