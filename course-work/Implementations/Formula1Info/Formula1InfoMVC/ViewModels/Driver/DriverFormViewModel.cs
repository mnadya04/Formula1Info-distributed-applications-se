using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Formula1InfoMVC.ViewModels.Driver
{

    public class DriverFormViewModel
    {
        public string? DriverId { get; set; }

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

        [Required]
        public string TeamId { get; set; }

        public IEnumerable<SelectListItem>? Teams { get; set; }
    }

}

