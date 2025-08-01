using AuthenLearn.Data.Common.QueryParameters;

namespace AuthenLearn.Applications.Helpers
{
    public static class TitleUpdateHelper
    {
        public static string GetTitleFromQuery(ExpenseQueryParameter query)
        {
            var parts = new List<string>();
            if (query.Day.HasValue)
            {
                parts.Add($"ngày: {query.Day.Value}");
            }
            if (query.Month.HasValue)
            {
                parts.Add($"tháng: {query.Month.Value}");
            }
            if (query.Year.HasValue)
            {
                parts.Add($"năm: {query.Year.Value}");
            }
            if (query.Paid.HasValue)
            {
                if (query.Paid.Value == true)
                {
                    parts.Add($"tình trạng (nợ): Đã trả");
                }
                else
                {
                    parts.Add($"tình trạng (nợ): Chưa trả");
                }

            }
            if (parts.Count == 0)
            {
                return "Toàn bộ danh sách chi tiêu";
            }
            return "Danh sách chi tiêu " + string.Join(" ", parts);
        }
    }
}