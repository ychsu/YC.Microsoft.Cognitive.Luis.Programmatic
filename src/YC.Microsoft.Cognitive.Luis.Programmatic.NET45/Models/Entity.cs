using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace YC.Microsoft.Cognitive.Luis.Programmatic.Models
{
    public class Entity
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class ClosedList
        : Entity
    {
        [JsonProperty("subLists")]
        public IEnumerable<ClosedListItem> SubLists { get; set; }
    }

    public class ClosedListItem
    {
		[JsonIgnore]
		public int Id { get; set; }

		[JsonProperty("id")]
		internal int ItemId { set => Id = value; }

		[JsonProperty("canonicalForm")]
        public string CanonicalForm { get; set; }

        [JsonProperty("list")]
        public IList<string> List { get; set; } = new List<string>();
    }
}
