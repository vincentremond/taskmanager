using System.Collections.Generic;

namespace TaskManager.Contract.ViewModel.Model.Context
{
    public class Index
    {
        public IEnumerable<Item> Items { get; set; }
        public class Item
        {
            public string ContextId { get; set; }
            public string Title { get; set; }
        }
    }
}
