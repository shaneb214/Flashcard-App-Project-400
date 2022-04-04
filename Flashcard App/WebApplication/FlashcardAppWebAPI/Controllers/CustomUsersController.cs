using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FlashcardAppWebAPI.Models;

namespace FlashcardAppWebAPI.Controllers
{
    public class CustomUsersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/CustomUsers
        public IQueryable<CustomUser> GetCustomUsers()
        {
            return db.CustomUsers;
        }

        // GET: api/CustomUsers/5
        [ResponseType(typeof(CustomUser))]
        public async Task<IHttpActionResult> GetCustomUser(string id)
        {
            CustomUser customUser = await db.CustomUsers.FindAsync(id);
            if (customUser == null)
            {
                return NotFound();
            }

            return Ok(customUser);
        }

        [Route("api/CustomUsers/GetUserDTO")]
        [ResponseType(typeof(UserDTO))]
        public async Task<IHttpActionResult> GetUserDTO(string id)
        {
            CustomUser customUser = await db.CustomUsers.FindAsync(id);           

            if (customUser == null)
            {
                return NotFound();
            }

            UserDTO userDTO = new UserDTO() { ID = customUser.ID, Username = customUser.User.UserName, Email = customUser.User.Email };

            return Ok(userDTO);
        }

        // PUT: api/CustomUsers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCustomUser(string id, CustomUser customUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customUser.ID)
            {
                return BadRequest();
            }

            db.Entry(customUser).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomUserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/CustomUsers
        [ResponseType(typeof(CustomUser))]
        public async Task<IHttpActionResult> PostCustomUser(CustomUser customUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CustomUsers.Add(customUser);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CustomUserExists(customUser.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = customUser.ID }, customUser);
        }

        // DELETE: api/CustomUsers/5
        [ResponseType(typeof(CustomUser))]
        public async Task<IHttpActionResult> DeleteCustomUser(string id)
        {
            CustomUser customUser = await db.CustomUsers.FindAsync(id);
            if (customUser == null)
            {
                return NotFound();
            }

            db.CustomUsers.Remove(customUser);
            await db.SaveChangesAsync();

            return Ok(customUser);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomUserExists(string id)
        {
            return db.CustomUsers.Count(e => e.ID == id) > 0;
        }
    }
}