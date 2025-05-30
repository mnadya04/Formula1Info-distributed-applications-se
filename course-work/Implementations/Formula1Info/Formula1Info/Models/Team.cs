using System;
namespace Formula1Info.Models
{
	public class Team
	{
        public Team()
        {
            this.TeamId = Guid.NewGuid().ToString();
        }
        public string TeamId { get; set; }
        public string Name { get; set; }
		public string BaseCountry { get; set; }
		public int FounderYear { get; set; }
		public string Description { get; set; }
		public decimal Budget { get; set; }
		public string ImageUrl { get; set; }
	}
}

