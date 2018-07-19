using System;

namespace TaskManager.Models
{
    [Serializable]
    public class Project
    {
        public string ProjectId { get; set; }
        public string Title { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateModified { get; set; }
        public string Color { get; set; }
    }
}
