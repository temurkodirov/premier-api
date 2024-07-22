namespace FSSEstate.Core.Utility.Listing
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string Order { get; set; }

        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }
}
