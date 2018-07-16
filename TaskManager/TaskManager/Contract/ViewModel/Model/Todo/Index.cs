using System.Collections.Generic;

namespace TaskManager.Contract.ViewModel.Model.Todo
{
    public class Index
    {
        public IEnumerable<Item> Items { get; set; }
        public class Item
        {
            public string TodoId { get; set; }
            public decimal MetaScore { get; set; }
            public string Title { get; set; }
        }
    }
}