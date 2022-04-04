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
    public class LanguageProfilesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/LanguageProfiles
        public IQueryable<LanguageProfile> GetLanguageProfiles()
        {
            return db.LanguageProfiles;
        }

        // GET: api/LanguageProfiles/5
        [ResponseType(typeof(LanguageProfile))]
        public async Task<IHttpActionResult> GetLanguageProfile(string id)
        {
            LanguageProfile languageProfile = await db.LanguageProfiles.FindAsync(id);
            if (languageProfile == null)
            {
                return NotFound();
            }

            return Ok(languageProfile);
        }

        [Route("api/LanguageProfiles")]
        [ResponseType(typeof(List<LanguageProfile>))]
        public async Task<IHttpActionResult> GetLanguageProfilesOfUser(string userID)
        {
            CustomUser user = await db.CustomUsers.FindAsync(userID);
            var languageProfiles = GetLanguageProfiles();
            List<LanguageProfile> languageProfilesOfUser = await languageProfiles.Where(langProfile => langProfile.userID == userID).ToListAsync();

            if (languageProfilesOfUser == null)
            {
                return NotFound();
            }

            return Ok(languageProfilesOfUser);
        }



        // PUT: api/LanguageProfiles/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLanguageProfile(string id, LanguageProfile languageProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != languageProfile.ID)
            {
                return BadRequest();
            }

            db.Entry(languageProfile).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageProfileExists(id))
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

        // POST: api/LanguageProfiles
        [ResponseType(typeof(LanguageProfile))]
        public async Task<IHttpActionResult> PostLanguageProfile(LanguageProfile languageProfile)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.LanguageProfiles.Add(languageProfile);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LanguageProfileExists(languageProfile.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = languageProfile.ID }, languageProfile);
        }

        // DELETE: api/LanguageProfiles/5
        [ResponseType(typeof(LanguageProfile))]
        public async Task<IHttpActionResult> DeleteLanguageProfile(string id)
        {
            LanguageProfile languageProfile = await db.LanguageProfiles.FindAsync(id);
            if (languageProfile == null)
            {
                return NotFound();
            }

            db.LanguageProfiles.Remove(languageProfile);
            await db.SaveChangesAsync();

            return Ok(languageProfile);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguageProfileExists(string id)
        {
            return db.LanguageProfiles.Count(e => e.ID == id) > 0;
        }
    }
}