using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Formula1InfoMVC.ViewModels.Race
{
    public class RaceFormViewModel
    {
        public string? RaceId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public string Location { get; set; } = null!;

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public int Laps { get; set; }

        [Url]
        [Display(Name = "Circuit Image URL")]
        public string CircuitUrl { get; set; } = null!;

        public string? DriverId { get; set; } = null!;

        public IEnumerable<SelectListItem>? Drivers { get; set; }

    }
}
