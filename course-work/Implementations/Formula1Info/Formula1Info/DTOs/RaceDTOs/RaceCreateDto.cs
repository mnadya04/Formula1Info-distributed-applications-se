using System;
using System.ComponentModel.DataAnnotations;

namespace Formula1Info.DTOs.RaceDTOs
{
    public class RaceCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int Laps { get; set; }
        public string CircuitUrl { get; set; }
        public string? DriverId { get; set; } // Winner
    }

}

