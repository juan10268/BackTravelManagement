using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTravel.Models
{
    public class Person
    {
        [Key]
        public int personID { get; set; }
        [Required]
        public int personIdentification { get; set; }
        [Required]
        public string personName { get; set; }
        [Required]
        public string personTypeDocument { get; set; }
        [Required]
        public DateTime personDateBirth { get; set; }
        [Required]
        public String personGender { get; set; }
        [Required]
        public string personEmail { get; set; }
        [Required]
        public string personPhone { get; set; }
        [Required]
        public string personEmergencyContactName { get; set; }
        [Required]
        public string personEmergencyContactPhone { get; set; }
        public ICollection<Reservation> ReservationPerson { get; set; }
        public ICollection<User> UserPerson { get; set; }
    }
}
