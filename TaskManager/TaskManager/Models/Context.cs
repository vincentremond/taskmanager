using System;

namespace TaskManager.Models
{
    [Serializable]
    public class Context
    {
        public string ContextId { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Color { get; set; }
    }
}
