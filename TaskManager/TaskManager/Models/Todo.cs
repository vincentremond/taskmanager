using System;

namespace TaskManager.Models
{
    public class Todo
    {
        public string TodoId { get; set; }
        public bool Completed { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public int Complexity { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Description { get; set; }
        public Context Context { get; set; }
    }
}
