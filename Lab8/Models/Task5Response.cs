namespace Lab8.Models
{
    public class Task5Response
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? Passport { get; set; }
        public string? License { get; set; }
        public string? PhoneNumber { get; set; }
        public long Count { get; set; }
        public decimal Avg { get; set; }
        public long Max { get; set; }
    }
}
