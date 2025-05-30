using System;
using Formula1InfoMVC.ViewModels.Helpers;

namespace Formula1InfoMVC.ViewModels.Team
{
	public class TeamIndexViewModel
	{
        public List<TeamViewModel> Teams { get; set; } = new();
        public string? SearchName { get; set; }
        public string? SearchCountry { get; set; }
        public PagerVM Pager { get; set; } = new();
    }
}

