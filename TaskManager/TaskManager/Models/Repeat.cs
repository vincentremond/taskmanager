using System;

namespace TaskManager.Models
{
    [Serializable]
    public class Repeat
    {
        public RepeatType Type { get; set; }
        public int Count { get; set; }
        public RepeatUnit Unit { get; set; }
    }
}
