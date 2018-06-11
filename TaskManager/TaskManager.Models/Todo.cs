using System;

namespace TaskManager.Models
{
    public class Todo
    {
        public Guid TodoId { get; set; }
        public bool Completed { get; set; }
        public string Title { get; set; }
    }
}
