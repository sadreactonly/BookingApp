using BookingApp.Models;
using BookingApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace BookingApp.Services
{
	public class CommentService : ICommentService
	{
		private IBAContext db;

		public CommentService()
		{
			db = new BAContext();
		}
		public bool Add(Comment comment)
		{
			db.Comments.Add(comment);
			db.SaveChanges();
			return true;
		}

		public bool Delete(int id)
		{
			Comment comment = db.Comments.Find(id);
			if (comment == null)
			{
				return false;
			}

			db.Comments.Remove(comment);
			db.SaveChanges();
			return true;

		}

		public void Dispose()
		{
			db.Dispose();
		}

		public IEnumerable<Comment> GetAll()
		{
			return db.Comments.Include(x=>x.Accomodation.AccommodationType).
				Include(x => x.Accomodation.Place).
				Include(x => x.Accomodation.Rooms);
		}

		public Comment GetById(int id)
		{
			Comment comment = db.Comments.Find(id);
			return comment;
		}

		public bool Update(int id, Comment comment)
		{
			db.MarkAsModified(comment);

			try
			{
				db.SaveChanges();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CommentExists(id))
				{
					return false;
				}
				else
				{
					throw;
				}
			}
			return true;
		}
		private bool CommentExists(int id)
		{
			return db.Comments.Count(e => e.Id == id) > 0;
		}
	}
}