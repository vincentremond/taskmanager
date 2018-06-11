namespace TaskManager.Models
{
    public class Todo
    {
        public decimal MetaScore { get; set; }
        public string TodoId { get; set; }
        public TodoStatus Status { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
    }
}
