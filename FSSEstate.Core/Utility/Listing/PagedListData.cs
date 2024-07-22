namespace FSSEstate.Core.Utility.Listing
{
    public class PagedListData<T>
    {
        public MetaData MetaData { get; set; }
        public PagedList<T> Items { get; set; }
    }
}
