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
            public string Context { get; set; }
            public string Project { get; set; }
        }

        public class DraftInfos
        {
            public int Count { get; set; }
            public string FirstTodoId { get; set; }
        }
    }
}