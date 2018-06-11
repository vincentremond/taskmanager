using System;
using System.Collections.Generic;

namespace TaskManager.Contract.ViewModel.Model
{
    public class TodoIndexViewModel
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