using BookingApp.Models;
using BookingApp.Services.Interfaces;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace BookingApp.Controllers
{
	public class CommentsController : ApiController
	{
		ICommentService commentService;

		public CommentsController(ICommentService commentService)
		{
			this.commentService = commentService;
		}

		// GET: api/Comments
		public IEnumerable<Comment> GetComments()
		{
			return commentService.GetAll();
		}

		// GET: api/Comments/5
		[ResponseType(typeof(Comment))]
		public IHttpActionResult GetComment(int id)
		{
			Comment comment = commentService.GetById(id);
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


			if (commentService.Update(id, comment))
			{
				return NotFound();
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

			if (!commentService.Add(comment))
			{
				return BadRequest();
			}

			return CreatedAtRoute("DefaultApi", new { id = comment.Id }, comment);
		}

		// DELETE: api/Comments/5
		[ResponseType(typeof(Comment))]
		public IHttpActionResult DeleteComment(int id)
		{
			if (!commentService.Delete(id))
			{
				return NotFound();
			}

			return Ok();
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				commentService.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}