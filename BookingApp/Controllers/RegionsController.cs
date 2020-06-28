using BookingApp.Models;
using BookingApp.Services.Interfaces;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class RegionsController : ApiController
	{

		private IRegionService regionService;

		public RegionsController(IRegionService regionService)
		{
			this.regionService = regionService;
		}

		// GET: api/Regions
		public IQueryable<Region> GetRegions()
		{
			return (IQueryable<Region>)regionService.GetAll();
		}

		// GET: api/Regions/5
		[ResponseType(typeof(Region))]
		public IHttpActionResult GetRegion(int id)
		{
			Region region = regionService.GetById(id);
			if (region == null)
			{
				return NotFound();
			}

			return Ok(region);
		}

		// PUT: api/Regions/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutRegion(int id, Region region)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != region.Id)
			{
				return BadRequest();
			}

			regionService.Update(id, region);

			return StatusCode(HttpStatusCode.NoContent);
		}

		// POST: api/Regions
		[ResponseType(typeof(Region))]
		public IHttpActionResult PostRegion(Region region)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
            
            //if(RegionExists(region.Id))
            //{
            //    return BadRequest();
            //}

			regionService.Add(region);

			return CreatedAtRoute("DefaultApi", new { id = region.Id }, region);
		}

		// DELETE: api/Regions/5
		[ResponseType(typeof(Region))]
		public IHttpActionResult DeleteRegion(int id)
		{
			Region region = regionService.GetById(id);
			if (region == null)
			{
				return NotFound();
			}

			regionService.Delete(id);

			return Ok(region);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				regionService.Dispose();
			}
			base.Dispose(disposing);
		}

	}
}