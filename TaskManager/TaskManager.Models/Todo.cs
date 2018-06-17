using System;

namespace TaskManager.Models
{
    public class Todo
    {
        public string TodoId { get; set; }
        public TodoStatus Status { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
    }
}
