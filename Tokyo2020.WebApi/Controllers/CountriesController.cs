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
    public class CountriesController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/Countries
        public IEnumerable<Country> GetCountries([FromUri]PagingParameterModel pagingparametermodel)
        {
            var source = (from gen in db.Countries.
                  OrderBy(a => a.IdCountry)
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

        // GET: api/Countries/5
        [ResponseType(typeof(Country))]
        public async Task<IHttpActionResult> GetCountry(int id)
        {
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
        }

        // PUT: api/Countries/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCountry(int id, Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != country.IdCountry)
            {
                return BadRequest();
            }

            db.Entry(country).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryExists(id))
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

        // POST: api/Countries
        [ResponseType(typeof(Country))]
        public async Task<IHttpActionResult> PostCountry(Country country)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Countries.Add(country);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = country.IdCountry }, country);
        }

        // DELETE: api/Countries/5
        [ResponseType(typeof(Country))]
        public async Task<IHttpActionResult> DeleteCountry(int id)
        {
            Country country = await db.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }

            db.Countries.Remove(country);
            await db.SaveChangesAsync();

            return Ok(country);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CountryExists(int id)
        {
            return db.Countries.Count(e => e.IdCountry == id) > 0;
        }
    }
}