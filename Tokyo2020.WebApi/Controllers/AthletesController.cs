﻿using System;
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
using Tokyo2020.Domain.Models;

namespace Tokyo2020.WebApi.Controllers
{
    public class AthletesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Athletes
        public IQueryable<Athlete> GetAthletes()
        {
            return db.Athletes;
        }

        // GET: api/Athletes/5
        [ResponseType(typeof(Athlete))]
        public async Task<IHttpActionResult> GetAthlete(int id)
        {
            Athlete athlete = await db.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }

            return Ok(athlete);
        }

        // PUT: api/Athletes/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutAthlete(int id, Athlete athlete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != athlete.IdAthlete)
            {
                return BadRequest();
            }

            db.Entry(athlete).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AthleteExists(id))
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

        // POST: api/Athletes
        [ResponseType(typeof(Athlete))]
        public async Task<IHttpActionResult> PostAthlete(Athlete athlete)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Athletes.Add(athlete);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = athlete.IdAthlete }, athlete);
        }

        // DELETE: api/Athletes/5
        [ResponseType(typeof(Athlete))]
        public async Task<IHttpActionResult> DeleteAthlete(int id)
        {
            Athlete athlete = await db.Athletes.FindAsync(id);
            if (athlete == null)
            {
                return NotFound();
            }

            db.Athletes.Remove(athlete);
            await db.SaveChangesAsync();

            return Ok(athlete);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AthleteExists(int id)
        {
            return db.Athletes.Count(e => e.IdAthlete == id) > 0;
        }
    }
}