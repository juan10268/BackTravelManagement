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
    [Route("TravelManagement/Person")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly ManagementTravelContext _context;

        public PeopleController(ManagementTravelContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
        {
            return await _context.Person.ToListAsync();
        }

        [EnableCors("AllowOrigin")]
        [Route("AddPerson")]
        [HttpPost]
        public async Task<ActionResult<Person>> addPerson(Person oPerson) {
            try {
                if (oPerson.personIdentification.Equals(null) || oPerson.personTypeDocument != null) {
                    _context.Person.Add(oPerson);
                    await _context.SaveChangesAsync();
                } else {
                    throw new Exception("The person data isnt fine");
                }
            } catch (Exception e){
                throw new Exception("Error-->", e);
            }
            return CreatedAtAction("GetPerson", new { id = oPerson.personID }, oPerson);
        }

        [EnableCors("AllowOrigin")]
        [Route("SearchForDocument")]
        [HttpGet]
        public bool checkIDAvailability(int personIdentification) {
            return _context.Person.Any(x => x.personIdentification == personIdentification);
        }

        [EnableCors("AllowOrigin")]
        [Route("PersonByID")]
        [HttpGet]
        public async Task<IEnumerable<Person>> getPersonById(int idPerson) {
            IEnumerable<Person> oLstHotels;
            try {
                oLstHotels = await _context.Person.ToListAsync();
                if (oLstHotels.Count() > 0) {
                    if (!idPerson.Equals(null)) {
                        oLstHotels = oLstHotels.Where(x => x.personIdentification == idPerson);
                    }
                }
            } catch (Exception e) {
                throw new Exception("Error-->", e);
            }
            return oLstHotels;
        }

        [EnableCors("AllowOrigin")]
        [Route("SearchForEmail")]
        [HttpGet]
        public bool checkMailAvailability(string personEmail) {
            return _context.Person.Any(x => x.personEmail == personEmail);
        }       
    }
}
