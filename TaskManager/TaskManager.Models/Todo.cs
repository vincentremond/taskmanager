using System;

namespace TaskManager.Models
{
    public class Todo
    {
        public decimal MetaScore { get; set; }
        public Guid TodoId { get; set; }
        public bool Completed { get; set; } // todo: replace with status
        public int Score { get; set; }
        public string Title { get; set; }
    }
}
