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
    public class FlashcardsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Flashcards
        public IQueryable<Flashcard> GetFlashcards()
        {
            return db.Flashcards;
        }

        // GET: api/Flashcards/5
        [ResponseType(typeof(Flashcard))]
        public async Task<IHttpActionResult> GetFlashcard(string id)
        {
            Flashcard flashcard = await db.Flashcards.FindAsync(id);
            if (flashcard == null)
            {
                return NotFound();
            }

            return Ok(flashcard);
        }

        //GET: api/Flashcards/5
        [ResponseType(typeof(List<Flashcard>))]
        public async Task<IHttpActionResult> GetFlashcardsOfLanguageProfile(string languageProfileID)
        {
            LanguageProfile languageProfile = await db.LanguageProfiles.FindAsync(languageProfileID);
            List<Set> setsOfLanguageProfile = await db.Sets.Where(set => set.LanguageProfileID == languageProfileID).ToListAsync();

            List<Flashcard> flashcards = new List<Flashcard>();

            setsOfLanguageProfile.ForEach(set => flashcards.AddRange(set.Flashcards));

            if (flashcards == null)
            {
                return NotFound();
            }

            return Ok(flashcards);
        }

        // PUT: api/Flashcards/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFlashcard(string id, Flashcard flashcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != flashcard.Id)
            {
                return BadRequest();
            }

            db.Entry(flashcard).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FlashcardExists(id))
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

        // POST: api/Flashcards
        [ResponseType(typeof(Flashcard))]
        public async Task<IHttpActionResult> PostFlashcard(Flashcard flashcard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Flashcards.Add(flashcard);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FlashcardExists(flashcard.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = flashcard.Id }, flashcard);
        }

        // DELETE: api/Flashcards/5
        [ResponseType(typeof(Flashcard))]
        public async Task<IHttpActionResult> DeleteFlashcard(string id)
        {
            Flashcard flashcard = await db.Flashcards.FindAsync(id);
            if (flashcard == null)
            {
                return NotFound();
            }

            db.Flashcards.Remove(flashcard);
            await db.SaveChangesAsync();

            return Ok(flashcard);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FlashcardExists(string id)
        {
            return db.Flashcards.Count(e => e.Id == id) > 0;
        }
    }
}