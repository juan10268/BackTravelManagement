using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManagementTravel.Models;
using Microsoft.AspNetCore.Cors;
using System.Data.SqlClient;

namespace TravelManagement.Controllers
{
    [Route("TravelManagement/Hotel")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly ManagementTravelContext _context;

        public HotelsController(ManagementTravelContext context)
        {
            _context = context;
        }

        [EnableCors("AllowOrigin")]
        [Route("AddHotel")]
        [HttpPost]
        public async Task<ActionResult<Hotel>> addHotel(Hotel oHotel) {
            try {
                if (oHotel.addressHotel != null || !oHotel.locationHotel.Equals(null)) {
                    _context.Hotels.Add(oHotel);
                    await _context.SaveChangesAsync();
                }
                else {
                    throw new Exception("The hotel data isnt fine");
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return CreatedAtAction("GetHotel", new { id = oHotel.IDHotel }, oHotel);
        }

        [EnableCors("AllowOrigin")]
        [Route("UpdateHotel")]
        [HttpPost]
        public async Task<ActionResult<Hotel>> hotelUpdate(Hotel oHotel) {
            Hotel oHotelUpsert = oHotel;
            try {
                if (!oHotelUpsert.IDHotel.Equals(null)) {
                    _context.Hotels.Update(oHotelUpsert);
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The hotel data isnt fine");
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return CreatedAtAction("GetHotel", new { id = oHotelUpsert.IDHotel }, oHotelUpsert);
        }

        [EnableCors("AllowOrigin")]
        [Route("ListHotels")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotel>>> getHotels() {
            return await _context.Hotels.ToListAsync();
        }

        [EnableCors("AllowOrigin")]
        [Route("HotelsByLocation")]
        [HttpGet]
        public async Task<IEnumerable<Hotel>> getHotelsByLocationAsync(string locationHotel) {
            IEnumerable<Hotel> oLstHotels;
            try {
                oLstHotels = await _context.Hotels.ToListAsync();
                if (oLstHotels.Count() > 0) {
                    if (locationHotel != null) {
                        oLstHotels = oLstHotels.Where(x => x.locationHotel.Contains(locationHotel));
                    }
                }
            }
            catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return oLstHotels;
        }

        [EnableCors("AllowOrigin")]
        [Route("ChangeStatusHotel")]
        [HttpGet]
        public async Task<ActionResult<Hotel>> changeStatusHotel(int idHotel, bool newStatus) {
            Hotel oHotel;
            try {
                if (!idHotel.Equals(null) || !newStatus.Equals(null)) {
                    oHotel = await _context.Hotels.FindAsync(idHotel);
                    if (oHotel.locationHotel != null || oHotel.nameHotel != null) {
                        if (oHotel.availableHotel != newStatus) {
                            oHotel.availableHotel = newStatus;
                            _context.Hotels.Update(oHotel);
                        }
                    }
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The hotel data isnt fine");
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return oHotel;
        }
    }        
}
