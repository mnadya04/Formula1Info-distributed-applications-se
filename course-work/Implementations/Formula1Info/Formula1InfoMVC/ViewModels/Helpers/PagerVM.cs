namespace Formula1InfoMVC.ViewModels.Helpers
{
    public class PagerVM
    {
        public int ItemsPerPage { get; set; } = 10;
        public int PagesCount { get; set; }
        public int Page { get; set; } = 1;
    }
}
