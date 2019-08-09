using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTravel.Models
{
    public class User {
        [Key]
        public string userNameUser { get; set; }
        [Required]
        public Person UserPerson { get; set; }
        [Required]
        public string userNamePassword { get; set; }
    }
}
