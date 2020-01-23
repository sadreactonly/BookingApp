using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Models;

namespace BookingApp.Services
{
	public interface IAccommodationService:IRepositoryService<Accommodation>
	{
		IEnumerable<Accommodation> GetPopularAccommodations();
	}
}
