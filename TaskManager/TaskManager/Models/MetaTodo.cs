using System;

namespace TaskManager.Models
{
    public class MetaTodo : Todo
    {
        public decimal MetaScore { get; set; }
        public bool IsDraft { get; set; }
    }
}
