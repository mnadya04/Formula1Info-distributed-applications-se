using System.ComponentModel.DataAnnotations;

namespace Formula1InfoMVC.ViewModels.Team
{
    public class TeamFormViewModel
    {
        public string? TeamId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string BaseCountry { get; set; }

        public int FounderYear { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Budget { get; set; }

        [MaxLength(200)]
        public string ImageUrl { get; set; }
    }
}
