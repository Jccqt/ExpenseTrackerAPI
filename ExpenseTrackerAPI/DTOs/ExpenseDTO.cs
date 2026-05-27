namespace ExpenseTrackerAPI.DTOs
{
    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DateLogged { get; set; }
    }
}
