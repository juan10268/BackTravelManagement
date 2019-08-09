using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTravel.Models
{
    public class Room {
        [Key]
        public int IDRoom { get; set; }
        public Hotel oRoom { get; set; }
        [Required]
        public int IDRoomHotel { get; set; }
        [Required]
        public bool availableRoom { get; set; }
        [Required]
        public string roomName { get; set; }
        [Required]
        public decimal totalPriceRoom { get; set; }
        [Required]
        public int quantityRoom { get; set; }
        [Required]
        public int taxesPercentRooms { get; set; }
        [Required]
        public int basePriceRoom { get; set; }
        public ICollection<Reservation> ReservationRoom { get; set; }
    }
}
