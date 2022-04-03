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
    public class LanguagesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Languages
        public IQueryable<Language> GetLanguages()
        {
            return db.Languages;
        }

        // GET: api/Languages/5
        [ResponseType(typeof(Language))]
        public async Task<IHttpActionResult> GetLanguage(string id)
        {
            Language language = await db.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            return Ok(language);
        }

        // PUT: api/Languages/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLanguage(string id, Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != language.ISO)
            {
                return BadRequest();
            }

            db.Entry(language).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageExists(id))
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

        // POST: api/Languages
        [ResponseType(typeof(Language))]
        public async Task<IHttpActionResult> PostLanguage(Language language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Languages.Add(language);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LanguageExists(language.ISO))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = language.ISO }, language);
        }

        // DELETE: api/Languages/5
        [ResponseType(typeof(Language))]
        public async Task<IHttpActionResult> DeleteLanguage(string id)
        {
            Language language = await db.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }

            db.Languages.Remove(language);
            await db.SaveChangesAsync();

            return Ok(language);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LanguageExists(string id)
        {
            return db.Languages.Count(e => e.ISO == id) > 0;
        }
    }
}