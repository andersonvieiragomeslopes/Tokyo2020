using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Tokyo2020.Domain.Models;
using Tokyo2020.WebApi.Models;

namespace Tokyo2020.WebApi.Controllers
{
    public class AthletesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Athletes
        public IEnumerable<Athlete> GetAthletes([FromUri]PagingParameterModel pagingparametermodel)
        {
            var source = (from gen in db.Athletes.
                  OrderBy(a => a.IdAthlete)
                          select gen).AsQueryable();

            // Get's No of Rows Count 
            int count = source.Count();

            int CurrentPage = pagingparametermodel.pageNumber;

            // Parameter is passed from Query string if it is null then it default Value will be pageSize:20
            int PageSize = pagingparametermodel.pageSize;

            // Display TotalCount to Records to User
            int TotalCount = count;

            // Calculating Totalpage by Dividing (No of Records / Pagesize)
            int TotalPages = (int)Math.Ceiling(count / (double)PageSize);

            var items = source.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

            // if CurrentPage is greater than 1 means it has previousPage
            var previousPage = CurrentPage > 1 ? "Yes" : "No";

            // if TotalPages is greater than CurrentPage means it has nextPage
            var nextPage = CurrentPage < TotalPages ? "Yes" : "No";
            var paginationMetadata = new
            {
                totalCount = TotalCount,
                pageSize = PageSize,
                currentPage = CurrentPage,
                totalPages = TotalPages,
                previousPage,
                nextPage
            };

            HttpContext.Current.Response.Headers.Add("Paging-Headers", JsonConvert.SerializeObject(paginationMetadata));


            return items;
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