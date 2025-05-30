using System;
namespace Formula1InfoMVC.ViewModels.Driver
{
    public class DriversPagedResult
    {
        public List<DriverViewModel> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

}

