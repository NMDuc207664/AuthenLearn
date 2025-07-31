using AuthenLearn.Data.Common;

namespace AuthenLearn.Applications.Extensions
{
    public static class PagedResultExtensions
    {
        public static PagedResult<TOut> Map<TIn, TOut>(this PagedResult<TIn> source, Func<TIn, TOut> mapper)
        {
            return new PagedResult<TOut>
            {
                Items = source.Items.Select(mapper).ToList(),
                PageIndex = source.PageIndex,
                PageSize = source.PageSize,
                TotalItems = source.TotalItems,
                Title = source.Title
            };
        }
    }
}