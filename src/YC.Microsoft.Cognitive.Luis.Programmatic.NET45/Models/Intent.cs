using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
    public class Intent
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("typeId")]
        public int TypeId { get; set; }

        [JsonProperty("readableType")]
        public string ReadableType { get; set; }
    }
}
