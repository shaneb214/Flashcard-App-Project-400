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
    public class SetsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Sets
        public IQueryable<Set> GetSets()
        {
            return db.Sets;
        }

        // GET: api/Sets/5
        [ResponseType(typeof(Set))]
        public async Task<IHttpActionResult> GetSet(string id)
        {
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            return Ok(set);
        }

        [ResponseType(typeof(List<Set>))]
        public async Task<IHttpActionResult> GetSetsOfLanguageProfile(string languageProfileID)
        {
            LanguageProfile languageProfile = await db.LanguageProfiles.FindAsync(languageProfileID);

            List<Set> setsOfProfile = await GetSets().Where(set => set.LanguageProfileID == languageProfileID).ToListAsync();

            if (setsOfProfile == null)
            {
                return NotFound();
            }

            return Ok(setsOfProfile);
        }



        // PUT: api/Sets/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSet(string id, Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != set.ID)
            {
                return BadRequest();
            }

            db.Entry(set).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SetExists(id))
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

        // POST: api/Sets
        [ResponseType(typeof(Set))]
        public async Task<IHttpActionResult> PostSet(Set set)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sets.Add(set);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SetExists(set.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = set.ID }, set);
        }

        // DELETE: api/Sets/5
        [ResponseType(typeof(Set))]
        public async Task<IHttpActionResult> DeleteSet(string id)
        {
            Set set = await db.Sets.FindAsync(id);
            if (set == null)
            {
                return NotFound();
            }

            db.Sets.Remove(set);
            await db.SaveChangesAsync();

            return Ok(set);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SetExists(string id)
        {
            return db.Sets.Count(e => e.ID == id) > 0;
        }
    }
}