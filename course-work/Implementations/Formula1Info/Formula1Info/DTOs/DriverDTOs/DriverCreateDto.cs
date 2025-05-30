using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Formula1Info.DTOs.DriverDTOs
{
    public class DriverCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MaxLength(50)]
        public string Nationality { get; set; }
        public string DriverImageUrl { get; set; }
        [Required]
        public int UniqueNumber { get; set; }
        [Required]
        public int NumberOfChampionships { get; set; }
        public string TeamId { get; set; }
    }
}

