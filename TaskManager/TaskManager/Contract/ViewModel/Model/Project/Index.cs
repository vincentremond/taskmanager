using System.Collections.Generic;

namespace TaskManager.Contract.ViewModel.Model.Project
{
    public class Index
    {
        public IEnumerable<Item> Items { get; set; }
        public class Item
        {
            public string ProjectId { get; set; }
            public string Title { get; set; }
        }
    }
}
