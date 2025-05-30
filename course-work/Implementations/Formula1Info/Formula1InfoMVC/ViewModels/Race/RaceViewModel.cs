using System;
namespace Formula1InfoMVC.ViewModels.Race
{
    public class RaceViewModel
    {
        public string RaceId { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public DateTime Date { get; set; }
    }
}

