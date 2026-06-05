namespace ExpenseTrackerAPI.Models
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime DateLogged { get; set; } = DateTime.Now;
        public int Status { get; set; } = 1;
    }
}
