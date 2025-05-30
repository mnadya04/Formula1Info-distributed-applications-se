using System;
namespace Formula1Info.DTOs.RaceDTOs
{
    public class RaceDetailDto
    {
        public string RaceId { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public DateTime Date { get; set; }
        public int Laps { get; set; }
        public string CircuitUrl { get; set; }
        public string? DriverId { get; set; }
    }

}

