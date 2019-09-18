namespace BookingApp.Models
{
	public class Comment
	{
		public int Id { get; set; }
		public int Grade { get; set; }
		public string Text { get; set; }
		public BAIdentityUser User { get; set; }
		public Accommodation Accomodation { get; set; }

		public Comment()
		{
		}
	}
}