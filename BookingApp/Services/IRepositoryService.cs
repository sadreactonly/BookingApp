using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Services
{
	public interface IRepositoryService<T>
	{
		IEnumerable<T> GetAll();
		T GetById(int id);
		bool Add(T product);
		bool Update(int id,T product);

		bool Delete(int id);
		void Dispose();
	}
}
