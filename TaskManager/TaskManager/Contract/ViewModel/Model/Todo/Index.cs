using System.Collections.Generic;
using TaskManager.Models;

namespace TaskManager.Contract.ViewModel.Model.Todo
{
    public class Index
    {
        public IEnumerable<Item> Items { get; set; }
        public DraftInfos Drafts { get; set; }
        public class Item
        {
            public string TodoId { get; set; }
            public decimal MetaScore { get; set; }
            public string Title { get; set; }
            public Context Context { get; set; }
            public Project Project { get; set; }
            public string Url { get; set; }
            public bool HasRepeat { get; set; }
        }

        public class Context
        {
            public string ContextId { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
        }

        public class Project
        {
            public string ProjectId { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
        }

        public class DraftInfos
        {
            public int Count { get; set; }
            public string FirstTodoId { get; set; }
        }
    }
}
