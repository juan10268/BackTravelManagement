using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ManagementTravel.Models
{
    public class Hotel{
        [Key]
        public int IDHotel { get; set; }
        [Required]
        public string nameHotel { get; set; }
        [Required]
        public string locationHotel { get; set; }
        public bool availableHotel { get; set; }
        [Required]
        public string addressHotel { get; set; }
        public ICollection<Room> IDRoomHotel { get; set; }
    }
}
