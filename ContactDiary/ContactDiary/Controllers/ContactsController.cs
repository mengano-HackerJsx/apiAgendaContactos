using ContactDiary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ContactDiary.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AgendaTelefonicaContext _connection;

        public ContactsController(AgendaTelefonicaContext context)
        {
            _connection = context;
        }

        [HttpGet]
        public IActionResult GetContacts()
            => StatusCode(StatusCodes.Status200OK, _connection.Contactos.ToList());

        [HttpGet("name")]
        //[Route("GetContactByName")]
        public IEnumerable<Contacto> GetContactByName(string name)
            => _connection.Contactos.Where(obj => obj.NombreContacto.Contains(name));


        [HttpPost("contact")]
        public IActionResult CreateContact([FromBody]Contacto contact)
        {
            _connection.Contactos.Add(contact);
            _connection.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, contact);
        }

        [HttpPut("contact")]
        public IActionResult UpdateContact([FromBody] Contacto contact)
        {
            _connection.Contactos.Update(contact);
            _connection.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, contact);
        }

        [HttpDelete("id")]
        public IActionResult DeleteContact(int id)
        {
            var contact = _connection.Contactos.Find(id);

            _connection.Remove(contact);
            _connection.SaveChanges();
            return StatusCode(StatusCodes.Status200OK, "OK");
        }
    }
}
