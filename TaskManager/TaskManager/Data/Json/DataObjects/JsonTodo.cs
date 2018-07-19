using System;

namespace TaskManager.Data.Json.DataObjects
{
    public class JsonTodo
    {
        public string TodoId { get; set; }
        public bool Completed { get; set; }
        public int Score { get; set; }
        public string Title { get; set; }
        public DateTimeOffset? ReferenceDate { get; set; }
        public int Complexity { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Description { get; set; }
        public string ContextId { get; set; }
        public string ProjectId { get; set; }
        public string Url { get; set; }
    }
}
