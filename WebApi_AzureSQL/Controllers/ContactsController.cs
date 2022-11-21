using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi_AzureSQL.Data;
using WebApi_AzureSQL.Models;

namespace WebApi_AzureSQL.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ContactsController : Controller
    {
        private readonly ContactAPIDbContext dbContext;
        public ContactsController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet(Name = "GetContacts")]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }

        [HttpPost(Name = "AddContacts")]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest)
        {
            var contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Name = addContactRequest.Name,
                Address = addContactRequest.Address,
                Phone = addContactRequest.Phone,
                Email = addContactRequest.Email
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }

        [HttpPut]
        [Route("UpdateContacts/{id:guid}")]
        public async Task<IActionResult> UpdateContact([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact != null)
            {
                contact.Name = updateContactRequest.Name;
                contact.Address = updateContactRequest.Address;
                contact.Phone = updateContactRequest.Phone;
                contact.Email = updateContactRequest.Email;
                await dbContext.SaveChangesAsync();

                return Ok(contact);
            }
            return NotFound();

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetContact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact == null)
            {
                return NotFound();
            }
             return Ok(contact);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            var contact = dbContext.Contacts.Find(id);
            if (contact != null)
            {
                dbContext.Remove(id);
                await dbContext.SaveChangesAsync();
                return Ok(contact);
            }
            return NotFound(id);
           
        }
    }
}
    

