using System;
using System.ComponentModel.DataAnnotations;

namespace Formula1Info.DTOs.TeamDTOs
{
    public class TeamCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MaxLength(50)]
        public string BaseCountry { get; set; }
        [Required]
        public int FounderYear { get; set; }
        [MaxLength(450)]
        public string Description { get; set; }
        public decimal Budget { get; set; }
        public string ImageUrl { get; set; }
    }

}

