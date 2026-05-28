namespace ExpenseTrackerAPI.DTOs
{
    public class ExpenseCreateDTO
    {
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
