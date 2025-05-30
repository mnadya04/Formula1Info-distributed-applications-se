using System;
namespace Formula1Info.Models
{
	public class Driver
	{
        public Driver()
        {
            this.DriverId = Guid.NewGuid().ToString();
        }
        public string DriverId { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
		public DateTime DateOfBirth { get; set; }
        public string Nationality { get; set; }
		public string DriverImageUrl { get; set; }
		public int UniqueNumber { get; set; }
		public int NumberOfChampionships { get; set; }
		public string TeamId { get; set; }
	}
}

