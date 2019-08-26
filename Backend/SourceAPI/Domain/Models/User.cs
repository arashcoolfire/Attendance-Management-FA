using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using AttendanceApi.Domain.Enums;

namespace AttendanceApi.Domain.Models
{
    public class User
    {
        public int UserId { get; set; }
        [MaxLength(50)]
        //[Required]
        public string UserName { get; set; }
        [MaxLength(50)]
        [Required]
        public string Password { get; set; }
        public UserRole UserRole { get; set; }
        public bool UserActived { get; set; }
        [ForeignKey("PersonnelId")]
        public int PersonnelId { get; set; }
        public virtual Personnel Personnel { set; get; }

        [NotMapped]
        public string UserRoleName { get; set; }
        [NotMapped]
        public string UserActivedName { get; set; }
    }
}
