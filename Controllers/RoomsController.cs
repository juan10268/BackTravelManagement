using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagementTravel.Models;
using Microsoft.AspNetCore.Cors;

namespace TravelManagement.Controllers
{
    [Route("TravelManagement/Room")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ManagementTravelContext _context;

        public RoomsController(ManagementTravelContext context)
        {
            _context = context;
        }

        [EnableCors("AllowOrigin")]
        [Route("AddRoom")]
        [HttpPost]
        public async Task<ActionResult<Room>> addRoom(Room oNewRoom) {
            try {
                if (!oNewRoom.totalPriceRoom.Equals(null)) {
                    _context.Rooms.Add(oNewRoom);
                    await _context.SaveChangesAsync();
                }
                else {
                    throw new Exception("The person data isnt fine");
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return CreatedAtAction("GetPerson", new { id = oNewRoom.IDRoom }, oNewRoom.IDRoom);
        }

        [EnableCors("AllowOrigin")]
        [Route("ListRooms")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> getRooms() {
            return await _context.Rooms.ToListAsync();
        }

        [EnableCors("AllowOrigin")]
        [Route("SearchRoomsForHotel")]
        [HttpGet]
        public async Task<IEnumerable<Room>> getRoomsForHotels(int idHotel) {
            IEnumerable<Room> oLstRoomsPerHotel;
            try {
                oLstRoomsPerHotel = await _context.Rooms.ToListAsync();
                if (oLstRoomsPerHotel.Count() > 0) {
                    if (!idHotel.Equals(null)) {
                        oLstRoomsPerHotel = oLstRoomsPerHotel.Where(x => x.IDRoomHotel == idHotel);
                    }
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return oLstRoomsPerHotel;
        }

        [EnableCors("AllowOrigin")]
        [Route("ChangeStatusRoom")]
        [HttpGet]
        public async Task<ActionResult<Room>> changeStatusRoom(int idRoom, bool newStatus) {
            Room oRoom;
            try {
                if (!idRoom.Equals(null) || !newStatus.Equals(null)) {
                    oRoom = await _context.Rooms.FindAsync(idRoom);
                    if (oRoom.roomName != null || oRoom.totalPriceRoom.Equals(null)) {
                        if (oRoom.availableRoom != newStatus) {
                            oRoom.availableRoom = newStatus;
                            _context.Rooms.Update(oRoom);
                        }
                    }
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The room data isnt fine");
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return oRoom;
        }

        [EnableCors("AllowOrigin")]
        [Route("UpdateRoom")]
        [HttpPost]
        public async Task<ActionResult<Room>> hotelUpdate(Room oRoom) {
            Room oRoomUpsert = oRoom;
            try {
                if (!oRoomUpsert.IDRoom.Equals(null)) {
                    _context.Rooms.Update(oRoomUpsert);
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The hotel data isnt fine");
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return CreatedAtAction("GetHotel", new { id = oRoomUpsert.IDRoom }, oRoomUpsert);
        }
    }
}
