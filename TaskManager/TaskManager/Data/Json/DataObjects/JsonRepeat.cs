using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using TaskManager.Models;

namespace TaskManager.Data.Json.DataObjects
{
    public class JsonRepeat
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public RepeatType Type { get; set; }
        public int Count { get; set; }
        [JsonConverter(typeof(StringEnumConverter))]
        public RepeatUnit Unit { get; set; }
    }
}
