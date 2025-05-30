using System;
namespace Formula1Info.DTOs.TeamDTOs
{
    public class TeamDetailDto
    {
        public string TeamId { get; set; }
        public string Name { get; set; }
        public string BaseCountry { get; set; }
        public int FounderYear { get; set; }
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string ImageUrl { get; set; }
    }

}

