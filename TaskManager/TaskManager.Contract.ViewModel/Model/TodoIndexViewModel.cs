using System;
using System.Collections.Generic;

namespace TaskManager.Contract.ViewModel.Model
{
    public class TodoIndexViewModel
    {
        public IEnumerable<Item> Items { get; set; }
        public class Item
        {
            public Guid TodoId { get; set; }
            public bool Completed { get; set; }
            public string Title { get; set; }
        }
    }
}