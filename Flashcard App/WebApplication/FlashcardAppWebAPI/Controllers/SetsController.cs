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

        [Route("api/Sets/GetSetsOfUser")]
        [ResponseType(typeof(List<Set>))]
        public async Task<IHttpActionResult> GetSetsOfUser(string userID)
        {
            List<Set> setListOfUser = new List<Set> { };
            List<LanguageProfile> languageProfilesOfUser = await db.LanguageProfiles.Where(profile => profile.userID == userID).ToListAsync();

            foreach (var profile in languageProfilesOfUser)
            {
                setListOfUser.AddRange(await GetSets().Where(set => set.LanguageProfileID == profile.ID).ToListAsync());
            }

            if (setListOfUser == null)
            {
                return NotFound();
            }

            return Ok(setListOfUser);
        }

        [Route("api/Sets/ModifyDefaultSetValue")]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> ModifyDefaultSet(Set setToModify)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Set set = await db.Sets.FindAsync(setToModify.ID);
            set.IsDefaultSet = setToModify.IsDefaultSet;

            db.Entry(set).State = EntityState.Modified;

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

            return Ok();
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

            return Ok();

            //return CreatedAtRoute("DefaultApi", new { id = set.ID }, set);
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