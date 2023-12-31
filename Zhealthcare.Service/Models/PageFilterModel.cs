﻿namespace Zhealthcare.Service.Models
{
    public class PageFilterModel
    {
        public int Start { get; set; } = 0;
        public int PageSize { get; set; } = 10;
        public PatientFilter? Filters { get; set; }
        public int Order { get; set; } = 1;
        public string SortBy { get; set; } = string.Empty;
        public string SearchQuery { get; set; } = string.Empty;

    }

    public class PatientFilter
    {
        public string[]? ReviewStatus { get; set; }
        public string[]? QueryStatus { get; set; }
        public DateTime? AdmitStartDate { get; set; }
        public DateTime? AdmitEndDate { get; set; }
    }
}
