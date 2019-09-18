using BookingApp.Models;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class CommentsController : ApiController
	{
		private IBAContext db;

		public CommentsController()
		{
			db = new BAContext();
		}

		public CommentsController(IBAContext context)
		{
			db = context;
		}

		// GET: api/Comments
		public IQueryable<Comment> GetComments()
		{
			return db.Comments;
		}

		// GET: api/Comments/5
		[ResponseType(typeof(Comment))]
		public IHttpActionResult GetComment(int id)
		{
			Comment comment = db.Comments.Find(id);
			if (comment == null)
			{
				return NotFound();
			}

			return Ok(comment);
		}

		// PUT: api/Comments/5
		[ResponseType(typeof(void))]
		public IHttpActionResult PutComment(int id, Comment comment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			if (id != comment.Id)
			{
				return BadRequest();
			}

			db.MarkAsModified(comment);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CommentExists(id))
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

		// POST: api/Comments
		[ResponseType(typeof(Comment))]
		public IHttpActionResult PostComment(Comment comment)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			db.Comments.Add(comment);
			db.SaveChanges();

			return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
		}

		// DELETE: api/Comments/5
		[ResponseType(typeof(Comment))]
		public IHttpActionResult DeleteComment(int id)
		{
			Comment comment = db.Comments.Find(id);
			if (comment == null)
			{
				return NotFound();
			}

			db.Comments.Remove(comment);
			db.SaveChanges();

			return Ok(comment);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}

		private bool CommentExists(int id)
		{
			return db.Comments.Count(e => e.Id == id) > 0;
		}
	}
}