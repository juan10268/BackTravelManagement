using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTravel.Models
{
    public class Reservation {
        [Key]
        public int IDReservation { get; set; }
        public Person ReservationPerson { get; set; }
        public Room ReservationRoom { get; set; }
        [Required]
        public int reservationPersonID { get; set; }
        [Required]
        public int reservationRoomID { get; set; }
        [Required]
        public string phonePersonReservation { get; set; }
        [Required]
        public Boolean activeReservation { get; set; }
        [Required]
        public DateTime sinceReservation { get; set; }
        [Required]
        public DateTime untilReservation { get; set; }
        [Required]
        public int quantityPersonReservation { get; set; }
        public string descriptionReservation { get; set; }
    }
}
