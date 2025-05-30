﻿using System;
namespace Formula1InfoMVC.ViewModels.Helpers
{
    public class PagedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }
    }

}