using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace AttendanceApi.Domain.Models
{
    public class Personnel
    {
        public Personnel()
        {
            Users = new HashSet<User>();
            AttendanceTimes = new HashSet<AttendanceTime>();
            Days = new HashSet<Day>();
            ApplicantActionRequests = new HashSet<ActionRequest>();
            CorroborantActionRequests = new HashSet<ActionRequest>();
        }
        public int PersonnelId { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 3)]
        [Required]
        public string LastName { get; set; }
        [StringLength(10, MinimumLength = 10)]
        [Required]
        public string NationalCode { get; set; }
        [StringLength(20)]
        public string PhoneNo { get; set; }
        [StringLength(50)]
        public string Email { get; set; }
        public ICollection<User> Users { set; get; }
        public ICollection<AttendanceTime> AttendanceTimes { set; get; }
        public ICollection<Day> Days { set; get; }
        [InverseProperty("Applicant")]
        public ICollection<ActionRequest> ApplicantActionRequests { set; get; }
        [InverseProperty("Corroborant")]
        public ICollection<ActionRequest> CorroborantActionRequests { set; get; }
        [NotMapped]
        public bool HasCurrentUser { set; get; }
        [NotMapped]
        public User CurrentUser { set; get; }
    }
}
