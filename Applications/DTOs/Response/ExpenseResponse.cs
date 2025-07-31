namespace AuthenLearn.Applications.DTOs.Response
{
    public class ExpenseResponse
    {
        public string? Description { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Title { get; set; }
    }
}