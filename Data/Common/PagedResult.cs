namespace AuthenLearn.Data.Common
{
    public class PagedResult<TEntity>
    {
        public required ICollection<TEntity> Items { get; set; }
        public string Title { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalItems / (double)PageSize);
    }
}