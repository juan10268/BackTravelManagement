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
    [Route("TravelManagement/Reservations")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ManagementTravelContext _context;

        public ReservationsController(ManagementTravelContext context)
        {
            _context = context;
        }


        [EnableCors("AllowOrigin")]
        [Route("getAllReservations")]
        [HttpGet]
        public async Task<IEnumerable<object>> getReservations() {
            IEnumerable<Reservation> oLstReservations = await _context.Reservations.ToListAsync();
            IEnumerable<Person> oLstPerson = await _context.Person.ToListAsync();
            var getAllReservations = oLstReservations.Join(oLstPerson, x => x.reservationPersonID, y => y.personID, (x,y) =>  new {
                x.IDReservation,
                x.phonePersonReservation,
                x.quantityPersonReservation,
                x.reservationPersonID,
                x.reservationRoomID,
                x.sinceReservation,
                x.untilReservation,
                x.activeReservation,
                x.descriptionReservation,
                y.personName
            });             
            return getAllReservations;
        }

        [EnableCors("AllowOrigin")]
        [Route("SearchReservationsForPersonAndHotel")]
        [HttpGet]
        public async Task<IEnumerable<object>> getRoomsForHotelsAndHotel(string personId, string hotelId){
            IEnumerable<Reservation> oLstReservationsPerPerson;
            var getReservationsByHotel = (IEnumerable<object>) null;
            try {                
                oLstReservationsPerPerson = await _context.Reservations.ToListAsync();
                if (oLstReservationsPerPerson.Count() > 0) {
                    int idPerson;
                    int idHotel;
                    IEnumerable<Hotel> oLstHotels = await _context.Hotels.ToListAsync();
                    IEnumerable<Room> oLstRooms = await _context.Rooms.ToListAsync();
                    IEnumerable<Person> oLstPerson = await _context.Person.ToListAsync();
                    if (personId != "null" && hotelId != "null") {
                        idPerson = Int32.Parse(personId);
                        idHotel = Int32.Parse(hotelId);                       
                        getReservationsByHotel = oLstReservationsPerPerson.Join(oLstPerson.Where(x => x.personIdentification == idPerson), x => x.reservationPersonID, y => y.personID, (x, y) =>
                         new { x.IDReservation, x.sinceReservation, x.untilReservation, x.activeReservation, x.quantityPersonReservation ,x.reservationRoomID, x.phonePersonReservation, y.personName})
                            .Join(oLstRooms, x => x.reservationRoomID, y => y.IDRoom, (x, y) => new {
                                x.IDReservation,
                                x.sinceReservation,
                                x.untilReservation,
                                x.quantityPersonReservation,
                                x.activeReservation,
                                x.personName,
                                x.phonePersonReservation,
                                y.roomName,
                                y.IDRoomHotel
                            }).Join(oLstHotels.Where(x => x.IDHotel == idHotel), x => x.IDRoomHotel, y => y.IDHotel, (x, y) => new {
                                x.IDReservation,
                                x.sinceReservation,
                                x.quantityPersonReservation,
                                x.untilReservation,
                                x.activeReservation,
                                x.personName,
                                x.roomName,
                                x.phonePersonReservation
                            }).OrderBy(x => x.sinceReservation);
                    } else if(personId != "null" || hotelId == "null") {
                        idPerson = Int32.Parse(personId);
                        getReservationsByHotel = oLstReservationsPerPerson.Join(oLstPerson.Where(x => x.personIdentification == idPerson), x => x.reservationPersonID,
                            y => y.personID, (x, y) => new {
                                x.IDReservation,
                                x.reservationRoomID,
                                x.sinceReservation,
                                x.untilReservation,
                                x.quantityPersonReservation,
                                x.phonePersonReservation,
                                x.activeReservation,
                                y.personName
                            }).Join(oLstRooms, x=> x.reservationRoomID, y=> y.IDRoom, (x,y) => new {
                                x.IDReservation,
                                x.sinceReservation,
                                x.untilReservation,
                                x.quantityPersonReservation,
                                x.phonePersonReservation,
                                x.activeReservation,
                                x.personName,
                                y.roomName
                            }).OrderBy(x => x.sinceReservation);
                    } else if(hotelId != "null" || personId == "null") {
                        idHotel = Int32.Parse(hotelId);                        
                        getReservationsByHotel = oLstReservationsPerPerson.Join(oLstRooms, x => x.reservationRoomID, y => y.IDRoom, (x, y) => new {
                             x.IDReservation,
                             x.sinceReservation,
                             x.untilReservation,
                             x.activeReservation,
                             x.quantityPersonReservation,
                             x.phonePersonReservation,
                             x.reservationPersonID,
                             y.IDRoom,
                             y.IDRoomHotel,
                             y.roomName
                         }).Join(oLstHotels.Where(x => x.IDHotel == idHotel), x => x.IDRoomHotel, y => y.IDHotel, (x, y) => new {
                             x.IDReservation,
                             x.sinceReservation,
                             x.untilReservation,
                             x.activeReservation,
                             x.roomName,
                             x.quantityPersonReservation,
                             x.reservationPersonID,
                             x.phonePersonReservation
                         }).Join(oLstPerson, x=> x.reservationPersonID, y => y.personID, (x,y) => new {
                             x.IDReservation,
                             x.sinceReservation,
                             x.untilReservation,
                             x.activeReservation,
                             x.roomName,
                             x.quantityPersonReservation,
                             x.phonePersonReservation,
                             y.personName
                         }).OrderBy(x => x.sinceReservation);
                    }
                }
            }
            catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return getReservationsByHotel;
        }

        [EnableCors("AllowOrigin")]
        [Route("CancelReservation")]
        [HttpGet]
        public async Task<ActionResult<Reservation>> cancelReservation(int idReservation) {
            Reservation oReservation;
            try {
                if (!idReservation.Equals(null)) {
                    oReservation = await _context.Reservations.FindAsync(idReservation);
                    oReservation.activeReservation = false;
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The hotel data isnt fine");
                }
            } catch (Exception e) { 
                throw new Exception("Error-->", e);
            }
            return oReservation;
        }

        [EnableCors("AllowOrigin")]
        [Route("AddReservation")]
        [HttpPost]
        public async Task<ActionResult<Reservation>> addReservation(Reservation oReservation) {
            try {
                if (!oReservation.reservationPersonID.Equals(null) || !oReservation.reservationRoomID.Equals(null)) {
                    IEnumerable<Person> oPerson = await _context.Person.ToListAsync();
                    oPerson = oPerson.Where(x => x.personIdentification == oReservation.reservationPersonID);
                    foreach(Person person in oPerson) {
                        oReservation.reservationPersonID = person.personID;
                    }
                    _context.Reservations.Add(oReservation);
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The person data isnt fine");
                }
            }
            catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return CreatedAtAction("GetReservation", new { id = oReservation.IDReservation }, oReservation);
        }
    }
}
